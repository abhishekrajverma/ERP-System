using Microsoft.EntityFrameworkCore;
using StudentCrudApp.Models;
using StudentCurdApp.Data.Configurations;
using StudentCurdApp.Models;

namespace StudentCrudApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; } // if you have two DbSets, ensure they are properly configured

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
