using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IStockRepository
    {
        public Task<Stock?> GetByProductIdAsync(int productId);
    }
}
