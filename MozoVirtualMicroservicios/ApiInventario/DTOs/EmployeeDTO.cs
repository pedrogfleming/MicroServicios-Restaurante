using LibreriaWinniePod;

namespace ApiInventario.DTOs
{
    public class EmployeeDTO : ErrorsHandler
    {
        public int? Rol { get; set; }
        public string? Token { get; set; }
        public string? Usuario { get; set; }
        public dynamic ToResource()
        {
            return new
            {
                Rol = this.Rol,
                Token = this.Token,
                Usuario = this.Usuario
            };
        }
    }
}
