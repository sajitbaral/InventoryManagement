using InventoryManagement.Entities.Operation;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class OperationDbContext : DbContext
    {
        public OperationDbContext(DbContextOption<OperationDbContext>options) : base(options) 
        {
            
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SaleItem> SaleItems { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseItem> PurchaseItems { get; set; }

    }
}
