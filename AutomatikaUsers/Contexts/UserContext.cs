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
                .HasIndex(x => x.Name)
                .IsUnique();
            #endregion

            #region Software
            modelBuilder.Entity<Software>()
                .HasIndex(x => x.Name)
                .IsUnique();
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
            #endregion
            #endregion
        }

    }
}
