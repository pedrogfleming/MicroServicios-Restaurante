using ApiMesa.Domain.Models;

namespace ApiMesa.DTO_s
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        public DateTime CreationDate { get; set; }        
        public int Status { get; set; }
        public int WaiterId { get; set; }
        public int Customers { get; set; }
    }
}
