namespace InventoryManagement.Entities.Operation
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
