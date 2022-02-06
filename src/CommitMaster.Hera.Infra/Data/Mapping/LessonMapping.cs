using CommitMaster.Hera.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommitMaster.Hera.Infra.Data.Mapping;

public class LessonMapping : IEntityTypeConfiguration<Lesson>
{
    

    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(a => a.CreatedAt)
            .IsRequired();
        
        builder.Property(a => a.Title)
            .IsRequired()
            .HasColumnType("varchar(100)");
        
        builder.Property(a => a.Description)
            .IsRequired()
            .HasColumnType("varchar(255)");
        
        builder.OwnsOne(c => c.Video)
            .Property(p => p.VideoUrl)
            .HasColumnName("ImgUrl")
            .HasColumnType("varchar(100)");


        builder.HasOne(p => p.Module)
            .WithMany(m => m.Lessons);
    }
}
