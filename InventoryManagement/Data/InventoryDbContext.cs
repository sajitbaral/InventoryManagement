using InventoryManagement.Entities.Inventory;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p=>p.Category)              /*Product Has one category*/
                .WithMany()                         /* Category can have many products*/
                .HasForeignKey(p => p.CategoryId)   /* CategoryId is the foreign key */
                .OnDelete(DeleteBehavior.Restrict); /* If a category is deleted, products in that category will not be deleted */

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Stock>()
                .HasIndex(s=>s.ProductId)
                .IsUnique();

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithOne()
                .HasForeignKey<Stock>(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Product)
                .WithMany()
                .HasForeignKey(sm=> sm.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

           



        }
    }
}
