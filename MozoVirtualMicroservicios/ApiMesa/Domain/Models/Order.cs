using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMesa.Domain.Models
{
    [Table("Orders")]
    public class Order
    {
        [Column("Id")]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ServiceFee { get; set; }
        public int Status { get; set; }
        public int TableNumber { get; set; }
        public List<Product> Products { get; set; }
        public int WaiterId { get; set; }
        public int Customers { get; set; }
    }
}
