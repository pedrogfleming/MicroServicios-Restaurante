using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMesa.Domain.Models
{
    [Table("Mesa_Order")]
    public class Mesa_Order
    {
        public int Id { get; set; }        
        public int QuantityOfPerson { get; set; }
        public int MesaId { get; set; }
        public int WaiterId { get; set; }
        public int OrderId { get; set; }

    }
}
