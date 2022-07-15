using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMesa.Domain.Models
{
    [Table("Mesas")]
    public class Mesa
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool IsAvailable { get; set; }
        public int MaxCustomer { get; set; }
    }
}
