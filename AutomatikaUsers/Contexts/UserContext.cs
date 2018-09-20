using AutomatikaUsers.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatikaUsers.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<UserSoftware> UserSoftware { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relationships
            #region User
            modelBuilder.Entity<User>()
                .HasIndex(x => x.IdentityName)
                .IsUnique();

            modelBuilder.Entity<User>().HasData(new User() { Id = 1, IdentityName = "Sparin", FirstName = "Yuriy", LastName = "Medveditskov", Email = "sparin285@gmail.com" });
            modelBuilder.Entity<User>().HasData(new User() { Id = 2, IdentityName = "MockUser2" });
            modelBuilder.Entity<User>().HasData(new User() { Id = 3, IdentityName = "MockUser3" });
            #endregion

            #region Software
            modelBuilder.Entity<Software>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Software>().HasData(new Software() { Id = 1, Name = "Microsoft Office 2017" });
            modelBuilder.Entity<Software>().HasData(new Software() { Id = 2, Name = "Microsoft Visual Studio 2017" });
            modelBuilder.Entity<Software>().HasData(new Software() { Id = 3, Name = "CCleaner" });
            modelBuilder.Entity<Software>().HasData(new Software() { Id = 4, Name = "Discord" });
            #endregion

            #region UserSoftware
            //Many to many relationship
            modelBuilder.Entity<UserSoftware>()
                .HasKey(x => new { x.UserId, x.SoftwareId });

            modelBuilder.Entity<UserSoftware>()
                .HasOne(x => x.User)
                .WithMany(x => x.InstalledSoftware)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<UserSoftware>()
                .HasOne(x => x.Software)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.SoftwareId);

            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 1, SoftwareId = 1 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 1, SoftwareId = 2 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 1, SoftwareId = 3 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 1, SoftwareId = 4 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 2, SoftwareId = 1 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 2, SoftwareId = 2 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 2, SoftwareId = 4 });
            modelBuilder.Entity<UserSoftware>().HasData(new UserSoftware() { UserId = 3, SoftwareId = 3 });
            #endregion
            #endregion
        }

    }
}
