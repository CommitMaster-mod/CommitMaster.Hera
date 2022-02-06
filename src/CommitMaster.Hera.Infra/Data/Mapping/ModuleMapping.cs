using CommitMaster.Hera.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Hera.Infra.Data.Mapping;

public class ModuleMapping : IEntityTypeConfiguration<Module>
{
    

    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(m => m.Order)
            .IsUnique();
        
        builder.Property(a => a.CreatedAt)
            .IsRequired();
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.HasMany(p => p.Lessons)
            .WithOne(p => p.Module);

        builder.HasOne(p => p.Course)
            .WithMany(m => m.Modules);
    }
}
