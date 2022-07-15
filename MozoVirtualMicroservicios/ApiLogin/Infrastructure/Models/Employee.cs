using System.ComponentModel.DataAnnotations.Schema;

namespace MozoVirtualMicroservicios.Login.Infrastucture.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public int? Rol { get; set; }
        public string? ApyNom { get; set; }
        public Int16 IsActive { get; set; }
    }
}
