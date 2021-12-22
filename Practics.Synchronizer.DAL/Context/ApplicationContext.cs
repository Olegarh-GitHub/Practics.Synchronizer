using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Practics.Synchronizer.Core.Models;

namespace Practics.Synchronizer.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonContacts> PersonContacts { get; set; }
        public DbSet<AwardStatus> AwardStatuses { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(x => x.ExtKey)
                .IsUnique();
            
            modelBuilder.Entity<Department>()
                .HasIndex(x => x.ExtKey)
                .IsUnique();

            modelBuilder.Entity<Worker>()
                .HasIndex(x => x.ExtKey)
                .IsUnique();

            modelBuilder.Entity<AwardStatus>()
                .HasIndex(x => x.ExtKey)
                .IsUnique();

            modelBuilder.Entity<PersonContacts>()
                .HasIndex(x => x.ExtKey)
                .IsUnique();
        }
    }
}