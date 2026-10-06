using Microsoft.EntityFrameworkCore;
using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using Operational.Infrastructure.Persistence;

namespace Operational.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly OperationalDbContext _context;
        public SaleRepository(OperationalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetSalesAsync(CancellationToken cancellationToken)
        {
            var sales = await _context.Sales
                .Include(s => s.SaleItems)
                .ToListAsync(cancellationToken);

            return sales;
        }

        public async Task<Sale?> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.SaleId == saleId, cancellationToken);

            return sale;
        }

        public void Add(Sale sale)
        {
            _context.Sales.Add(sale);
        }
    }
}
