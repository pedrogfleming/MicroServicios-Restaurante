using ApiMesa.DTO_s;

namespace ApiMesa.Services.IServices
{
    public interface IMesaServices
    {
        Task<bool> AbrirMesa(int orderId, int mesaId);
        Task<bool> CerrarMesa(int idMesa);
        Task<List<MesaDTO>> VerMesasDisponibles();

    }
}
