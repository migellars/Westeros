using Domain.Models.Audit;
using Domain.Models.Authors;
using Domain.Models.Lesson;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts;

public interface IWesterosContext
{
    DbSet<AuditTrail>? AuditLogs { get; set; }
    DbSet<Author>? Author { get; set; }
    DbSet<Tutorial>? Tutorials { get; set; }
    Task<int> SaveChangesAsync(string? userId = null);

}