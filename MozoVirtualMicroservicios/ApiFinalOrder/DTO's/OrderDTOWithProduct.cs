using LibreriaWinniePod;

namespace ApiFinalOrder.DTO_s
{
    public class OrderDTOWithProduct: ErrorsHandler
    {
        public int? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ServiceFee { get; set; }
        public int Status { get; set; }
        public int TableNumber { get; set; }
        public List<ProductDTO>? Products { get; set; }
        public int WaiterId { get; set; }
        public int Customers { get; set; }
    }
}
