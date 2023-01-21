using System.Reflection;
using Application.Contracts;
using Application.Dto;
using Domain.Models;
using Domain.Models.Audit;
using Domain.Models.Authors;
using Domain.Models.Lesson;
using Domain.Models.User;
using Infrastructure.EF.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Resources.Enum;

namespace Infrastructure.EF;

public class WesterosContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>, IWesterosContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

    public WesterosContext(DbContextOptions<WesterosContext> options) : base(options)
    {
    }

    public WesterosContext()
    {
    }

    public DbSet<AuditTrail>? AuditLogs { get; set; }
    public DbSet<Author>? Author { get; set; }
    public DbSet<Tutorial>? Tutorials { get; set; }

    protected override void OnModelCreating(ModelBuilder? modelBuilder)
    {
        if (modelBuilder == null)
            return;

        base.OnModelCreating(modelBuilder);
        foreach (var seeder in Assembly.GetExecutingAssembly().ExportedTypes
            .Where(type => type.BaseType == typeof(ISeeder)))
        {
            (Activator.CreateInstance(seeder) as ISeeder)?.Seed(modelBuilder);
        }

        modelBuilder.ApplyConfiguration(new AuthorConfiguration());

        //....Other Config

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        try
        {
            PerformEntityAudit();
            return base.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var proposedValues = entry.CurrentValues;
                var databaseValues = entry.GetDatabaseValues();

                foreach (var property in proposedValues.Properties)
                {
                    var proposedValue = proposedValues[property];
                    var databaseValue = databaseValues?[property];

                    // TODO: decide which value should be written to database
                    proposedValues[property] = proposedValue;
                }

                // Refresh original values to bypass next concurrency check
                if (databaseValues != null) entry.OriginalValues.SetValues(databaseValues);
            }

            PerformEntityAudit();
            return base.SaveChanges();
        }
    }

    public virtual async Task<int> SaveChangesAsync(string? userId = null)
    {
        PerformEntityAudit();
        OnBeforeSaveChanges(userId);
        var result = await base.SaveChangesAsync();
        return result;
    }

    private string GetModifiedUser()
    {
        var name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        return !string.IsNullOrEmpty(name) ? name : "Anonymous";
    }

    private string GetUserIpAddress()
    {
        var address = _httpContextAccessor.HttpContext?.Connection.LocalIpAddress.ToString();
        return address;
    }
    private void OnBeforeSaveChanges(string? userId)
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;
            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = userId,
                IpAddress = GetUserIpAddress()
            };
            auditEntries.Add(auditEntry);
            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        foreach (var auditEntry in auditEntries)
        {
            AuditLogs?.Add(auditEntry.ToAudit());
        }
    }
    private void PerformEntityAudit()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    var currentDateTime = DateTime.Now;
                    entry.Entity.CreatedBy = GetModifiedUser();
                    entry.Entity.DateCreated = currentDateTime;
                    entry.Entity.DateModified = currentDateTime;
                    entry.Entity.IsDeleted = false;
                    entry.Entity.IsActive = true;
                    entry.Entity.LastModifiedBy = null;
                    break;

                case EntityState.Modified:
                    entry.Entity.DateModified = DateTime.Now;
                    entry.Entity.IsActive = true;
                    entry.Entity.LastModifiedBy = GetModifiedUser();
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.LastModifiedBy = GetModifiedUser();
                    entry.Entity.DateModified = DateTime.Now;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.IsActive = false;
                    break;
            }
        }
    }

    public Task CompleteSaveAsync()
    {
        PerformEntityAudit();
        return base.SaveChangesAsync();
    }
}