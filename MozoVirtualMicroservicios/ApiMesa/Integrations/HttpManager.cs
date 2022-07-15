using System.Net;
using System.Text;
using System.Text.Json;
namespace ApiMesa.Integrations
{
    public class HttpManager : IHttpManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpManager(IHttpClientFactory client)
        {
            _clientFactory = client;
        }

        public async Task<(HttpStatusCode statusCode, TOutput output)> GetAsync<TOutput>(string url, string? token)
        {
            var http = _clientFactory.CreateClient();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var header = http.DefaultRequestHeaders.TryAddWithoutValidation("token", token);
            }
            var result = await http.GetFromJsonAsync<TOutput>(url);
            if (result != null)
            {
                return (HttpStatusCode.OK, result);
            }
            else
            {
                return (HttpStatusCode.BadGateway, result);
            }
        }

        public async Task<(HttpStatusCode statusCode, TOutput output)> PostAsync<TInput, TOutput>(string url, TInput body, string? token)
        {
            var data = this.GenerateContent(body);
            var http = _clientFactory.CreateClient();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var header = http.DefaultRequestHeaders.TryAddWithoutValidation("token", token);
            }
            var result = http.PostAsync(url, data);
            return await this.ReturnRequestResult<TOutput>(await result);
        }

        public async Task<(HttpStatusCode statusCode, TOutput output)> PutAsync<TInput, TOutput>(string url, TInput body, string? token)
        {
            var data = this.GenerateContent(body);
            var http = _clientFactory.CreateClient();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var header = http.DefaultRequestHeaders.TryAddWithoutValidation("token", token);
            }
            var result = http.PutAsync(url, data);
            return await this.ReturnRequestResult<TOutput>(await result);
        }

        private StringContent GenerateContent<T>(T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            return new StringContent(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
        }

        private async Task<(HttpStatusCode statusCode, T output)> ReturnRequestResult<T>(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                var s = result.Content.ReadAsStringAsync();
                var deserialized = (result.StatusCode, JsonSerializer.Deserialize<T>(s.Result));
                return (result.StatusCode, default);
            }
            return (result.StatusCode, default);
        }
    }
}
