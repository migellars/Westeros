using Domain.Models.Lesson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configuration;

public class LessonConfiguration: IEntityTypeConfiguration<Tutorial>
{
    public void Configure(EntityTypeBuilder<Tutorial> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).IsRequired();
        builder.Property(x=>x.Description).IsRequired();
        builder.HasOne(x => x.Author).WithMany(x=>x.Tutorials).Metadata.DeleteBehavior = DeleteBehavior.SetNull;
    }
}