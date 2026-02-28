using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Th_Dpt.Models;   // Use your actual namespace

namespace Th_Dpt.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MasterUser> MasterUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Evangelism> Students_Evang { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.MasterUserId, ur.RoleId });
            modelBuilder.Entity<Evangelism>()
        .ToTable("Students_Evang");
        }
    }
}
