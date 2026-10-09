
using System.ComponentModel.DataAnnotations;

    namespace Inventory.Application.DTOs.Stock
    {
        public class IncreaseStockDto
        {
            [Range(1, int.MaxValue)]
            public int ProductId {  get; set; }

            [Range(1, int.MaxValue)]
            public int Quantity {  get; set; }

            [Range(1, int.MaxValue)]
            public int ReferenceId {  get; set; }

        }
    }
