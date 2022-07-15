using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinalOrder.Domain.Model
{
    [Table("ProductNew")]
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int? Stock { get; set; }
        public int Category { get; set; }
    }
}
