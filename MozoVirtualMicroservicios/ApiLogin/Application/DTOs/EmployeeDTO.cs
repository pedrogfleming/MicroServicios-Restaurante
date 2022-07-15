namespace MozoVirtualMicroservicios.Login.Application.DTOs
{
    public class EmployeeDTO
    {
        public int? Id { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public int? Rol { get; set; }
        public string? ApyNom { get; set; }
        public Int16 IsActive { get; set; }
    }
}
