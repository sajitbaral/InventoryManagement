using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Operation;
using InventoryManagement.IService;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly OperationDbContext _context;
        public CustomerService(OperationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task<List<CustomerResponseDto>> GetCustomersAsync()
        {
            var customers = await _context.Customers
                .Select(c=> new CustomerResponseDto
                {

                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerResponseDto?>GetCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CustomerResponseDto
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address,
                    CreatedAt = c.CreatedAt
                })
            .FirstOrDefaultAsync();

            return customer;
        }
    }
}
