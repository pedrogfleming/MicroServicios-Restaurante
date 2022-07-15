using ApiFinalOrder.DTO_s;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Integrations;
using System.Net;

namespace ApiFinalOrder.ExternalServices
{
    public class ExternalServicesMesa : IExternalServicesMesa
    {
        private readonly IHttpManager _httpManager;
        private const string urlAbrir =
            "https://localhost:7090/gateway/Mesa/Abrir";
        private const string urlCerrar =
            "https://localhost:7090/gateway/Mesa/Cerrar";


        public ExternalServicesMesa(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<bool> AbrirMesa(
            AbrirMesaDTO abrirMesaDTO,
            string token)
        {
            try
            {
                var (statusCode, output) =
                    await _httpManager.PostAsync<AbrirMesaDTO, bool>(urlAbrir, abrirMesaDTO, token);

                if (statusCode == HttpStatusCode.OK && output)
                {
                    return output;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo asignar una mesa a la orden",ex.InnerException);
            }
        }

        public async Task<bool> CerrarMesa(
            int id,
            string token)
        {
            var (statusCode, output) =
                await _httpManager.PutAsync<int, bool>(urlCerrar, id, token);

            if (statusCode == HttpStatusCode.OK && output)
            {
                return output;
            }
            return false;
        }
    }
}
