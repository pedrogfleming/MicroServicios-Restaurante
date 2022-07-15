using System.Net;
using System.Threading.Tasks;

namespace ApiFinalOrder.Integrations
{
    public interface IHttpManager
    {
        Task<(HttpStatusCode statusCode, TOutput output)> GetAsync<TOutput>(string url, string? token);

        Task<(HttpStatusCode statusCode, TOutput output)> PostAsync<TInput, TOutput>(string url, TInput body, string? token);

        Task<(HttpStatusCode statusCode, TOutput output)> PutAsync<TInput, TOutput>(string url, TInput body, string? token);

    }
}
