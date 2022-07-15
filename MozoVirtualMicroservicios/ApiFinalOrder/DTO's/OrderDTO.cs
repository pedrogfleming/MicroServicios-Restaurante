using LibreriaWinniePod;

namespace ApiFinalOrder.DTO_s
{
    public class OrderDTO : ErrorsHandler
    {
        public int? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ServiceFee { get; set; }
        public int Status { get; set; }
        public string TableCode { get; set; }
        public int WaiterId { get; set; }
        public int Customers { get; set; }
    }
}
