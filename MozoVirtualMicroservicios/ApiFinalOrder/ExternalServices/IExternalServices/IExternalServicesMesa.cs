using ApiFinalOrder.DTO_s;

namespace ApiFinalOrder.ExternalServices.IExternalServices
{
    public interface IExternalServicesMesa
    {
        public Task<bool> AbrirMesa(AbrirMesaDTO abrirMesaDTO, string token);
        public Task<bool> CerrarMesa(int idMesa, string token);
    }
}
