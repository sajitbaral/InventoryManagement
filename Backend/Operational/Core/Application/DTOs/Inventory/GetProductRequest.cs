
using System.ComponentModel.DataAnnotations;


namespace Operational.Application.DTOs.Inventory
{
    public class GetProductRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
