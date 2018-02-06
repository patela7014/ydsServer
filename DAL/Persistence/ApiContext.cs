using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DAL.Core.Models;

namespace DAL.Persistence
{
    public partial class ApiContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        //public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<Mandal> Mandal { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Sabha> Sabha { get; set; }
        public virtual DbSet<SabhaType> SabhaType { get; set; }
        public virtual DbSet<SabhaUsers> SabhaUsers { get; set; }
        public virtual DbSet<EventAttendance> EventAttendance { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=YdsDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SabhaUsers>().HasKey(su =>
              new { su.SabhaId, su.UserId });

            modelBuilder.Entity<EventAttendance>().HasKey(ea =>
              new { ea.EventId, ea.UserId });
        }
    }
}
