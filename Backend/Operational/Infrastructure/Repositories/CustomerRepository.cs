using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using Operational.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OperationalDbContext _context;
        public CustomerRepository(OperationalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken)
        {
            var customers= await _context.Customers
                .ToListAsync(cancellationToken);

            return customers;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken)
        {
            var customer= await _context.Customers
                .FirstOrDefaultAsync(c=>c.CustomerId==customerId, cancellationToken);

            return customer;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }
    }
}
