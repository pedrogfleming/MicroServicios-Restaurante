using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMesa.Domain.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public int Category { get; set; }
        public int Quantity { get; set; }
    }
}
