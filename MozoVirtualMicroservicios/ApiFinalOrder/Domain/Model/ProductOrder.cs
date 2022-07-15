using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinalOrder.Domain.Model
{
    [Table("ProductOrder")]
    public class ProductOrder
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public int Quantity { get; set; }
    }
}
