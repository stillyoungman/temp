using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Persistence
{
    public class BarbershopContext : AbstractContext
    {
        public BarbershopContext(DbContextOptions options) : base(options)
        {

        }

        private DbSet<Post> Posts { get; set; }
        private DbSet<Administrator> Administrators { get; set; }
        private DbSet<Client> Clients { get; set; }
        private DbSet<Master> Masters { get; set; }
        private DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<User>();

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasMany(a => a.Posts)
                    .WithOne(p => p.Author)
                    .HasForeignKey(p => p.AuthorId)
                    .HasPrincipalKey(a => a.Id);

            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasMany(e => e.Appointments)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.ClientId)
                .HasPrincipalKey(e => e.Id);


            });

            modelBuilder.Entity<Master>(entity =>
            {
                entity.HasMany(e => e.Appointments)
                .WithOne(e => e.Master)
                .HasForeignKey(e => e.MasterId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(e => e.Service)
                .WithMany()
                .HasForeignKey(e => e.ServiceId)
                .HasPrincipalKey(e => e.Id);
            });

            modelBuilder.Entity<Service>(entity =>
            {

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
