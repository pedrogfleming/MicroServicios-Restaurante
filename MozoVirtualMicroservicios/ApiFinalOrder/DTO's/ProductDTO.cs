using LibreriaWinniePod;

namespace ApiFinalOrder.DTO_s
{
    public class ProductDTO : ErrorsHandler
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int? Stock { get; set; }
        public int Category { get; set; }
        public int Quantity { get; set; }
    }
}
