using Domain.Models.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configuration;

public class AuthorConfiguration: IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Address).Property(x => x.City).HasColumnName("City");
        builder.OwnsOne(x => x.Address).Property(x => x.Street).HasColumnName("Street Name");
        builder.OwnsOne(x => x.Address).Property(x => x.Lga).HasColumnName("Local Govt");
        builder.OwnsOne(x => x.Address).Property(x => x.State).HasColumnName("State");
        builder.OwnsOne(x => x.Address).Property(x => x.Country).HasColumnName("Country");
        builder.OwnsOne(x => x.Address).Property(x => x.ZipCode).HasColumnName("Postal Code");
        //builder.HasMany(x => x.Tutorials).WithOne(x => x.Author).Metadata.DeleteBehavior = DeleteBehavior.SetNull;
    }
}