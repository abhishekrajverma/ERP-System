using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentCrudApp.Models;

namespace StudentCurdApp.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
            public void Configure(EntityTypeBuilder<Student> builder)
            {
                builder.HasKey(s => s.Id);
                builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
                builder.Property(s => s.Email).IsRequired().HasMaxLength(100);
                builder.Property(s => s.Course).IsRequired().HasMaxLength(50);
            }
        
    }
}
