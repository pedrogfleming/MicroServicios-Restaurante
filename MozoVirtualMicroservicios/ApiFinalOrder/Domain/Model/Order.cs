using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinalOrder.Domain.Model
{

    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ServiceFee { get; set; }
        public int Status { get; set; }
        public int TableNumber { get; set; }
        public int WaiterId { get; set; }
        public int Customers { get; set; }
    }
}
