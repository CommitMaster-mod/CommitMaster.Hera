using CommitMaster.Hera.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Hera.Infra.Data.Mapping;

public class CourseMapping : IEntityTypeConfiguration<Course>
{

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(50)");
        
        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.OwnsOne(c => c.Cover)
            .Property(p => p.ImgUrl)
            .HasColumnName("ImgUrl")
            .HasColumnType("varchar(50)");
        
        builder.HasOne(c => c.Category)
            .WithMany(c => c.Courses)
            .HasForeignKey(u => u.CategoryId);
        
        builder.HasMany(c => c.Modules)
            .WithOne(c => c.Course)
            .HasForeignKey(u => u.CourseId);
    }
}
