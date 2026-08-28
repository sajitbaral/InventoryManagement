using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Operation;
using InventoryManagement.IService;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly OperationDbContext _context;
        public SupplierService(OperationDbContext context)
        {
            _context = context;
        }

        public async Task<SupplierResponseDto> CreateSupplierAsync(CreateSupplierDto dto)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow

            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return new SupplierResponseDto
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address,
                CreatedAt = supplier.CreatedAt
            };
        }

        public async Task<List<SupplierResponseDto>> GetSupplierAsync()
        {
            var suppliers = await _context.Suppliers
                .Select(s => new SupplierResponseDto
                {
                    SupplierId = s.SupplierId,
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email,
                    Address = s.Address,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();

            return suppliers;
        }

        public async Task<SupplierResponseDto?> GetSupplierByIdAsync(int supplierId)
        {
            var supplier= await _context.Suppliers
                .Where(s => s.SupplierId == supplierId)
                .Select(s => new SupplierResponseDto
                {
                    SupplierId= s.SupplierId,
                    Name= s.Name,
                    Phone= s.Phone,
                    Email= s.Email,
                    Address= s.Address,
                    CreatedAt= s.CreatedAt
                })
                .FirstOrDefaultAsync();

            return supplier;
        }
    }
}
