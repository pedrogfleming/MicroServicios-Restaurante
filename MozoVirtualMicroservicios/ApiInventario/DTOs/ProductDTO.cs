using ApiInventario.Domain.IModels;
using LibreriaWinniePod;

namespace ApiInventario.DTOs
{
    public class ProductDTO : ErrorsHandler, IProduct, IToResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Stock { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
        public bool IsAvailable { get; set; }
        public dynamic ToResource()
        {
            return new
            {
                Id = this.Id,
                Description = this.Description,
                Cost = this.Cost,
                Stock = this.Stock,
                Category = this.Category,
                Type = this.Type,
                IsAvailable = this.IsAvailable
            };
        }
    }
}
