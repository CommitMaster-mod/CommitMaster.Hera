using CommitMaster.Hera.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Hera.Infra.Data.Mapping;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{

    public void Configure(EntityTypeBuilder<Category> builder)
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
        
        builder.HasMany(c => c.Courses)
            .WithOne(c => c.Category);

    }
}
