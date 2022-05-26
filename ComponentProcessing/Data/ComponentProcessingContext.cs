using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComponentProcessing.Model;

namespace ComponentProcessingDbContext.Data
{
    public class ComponentProcessingContext : DbContext
    {
        public ComponentProcessingContext (DbContextOptions<ComponentProcessingContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProcessRequest>().HasNoKey();
        //    modelBuilder.Entity<ProcessRequest>(entity =>
        //    {
        //        entity.Property(s => s.Name)
        //        .HasColumnName("Name")
        //        .IsRequired();

        //        entity.Property(s => s.ContactNumber)
        //        .HasColumnName("ContactNumber")
        //        .IsRequired();

        //        entity.Property(s => s.ComponentType)
        //        .HasColumnName("ComponentType")
        //        .IsRequired();

        //        entity.Property(s => s.ComponentName)
        //        .HasColumnName("ComponentName")
        //        .IsRequired();

        //        entity.Property(s => s.Quantity)
        //        .HasColumnName("Quantity")
        //        .IsRequired();
        //    });

        //}

        public DbSet<ComponentProcessing.Model.ProcessResponse> ProcessResponse { get; set; }
        public DbSet<ComponentProcessing.Model.ProcessRequest> ProcessRequest { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }
    }
}
