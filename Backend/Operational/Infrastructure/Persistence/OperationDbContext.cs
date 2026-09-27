using Microsoft.EntityFrameworkCore;
using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Persistence
{
    public class OperationalDbContext : DbContext
    {
        public OperationalDbContext(DbContextOptions<OperationalDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SaleItem> SaleItems { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(254);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Email)
                .HasMaxLength(254);

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Name);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.TotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasIndex(p => p.PurchaseDate);

            modelBuilder.Entity<PurchaseItem>()
                .Property(p => p.SubTotal)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PurchaseItem>()
                .Property(p => p.UnitCost)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Purchase)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .Property(p => p.TotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasIndex(s => s.SaleDate);


            modelBuilder.Entity<SaleItem>()
                .Property(p => p.SubTotal)
                .HasPrecision(12, 2);

            modelBuilder.Entity<SaleItem>()
                .Property(p => p.UnitPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Restrict);





        }

    }
}
