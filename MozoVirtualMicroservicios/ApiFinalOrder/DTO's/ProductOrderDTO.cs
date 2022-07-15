using LibreriaWinniePod;

namespace ApiFinalOrder.DTO_s
{
    public class ProductOrderDTO : ErrorsHandler
    {
        public int? Id { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public int Quantity { get; set; }
    }
}
