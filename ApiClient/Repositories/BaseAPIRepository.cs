using System.Net.Http.Headers;

namespace ApiClient.Repositories
{
    public class BaseAPIRepository
    {
        protected HttpClient client;
        protected DelegatingHandler? _handler;
        protected string? _baseUrl;
        protected string? _path;

        public BaseAPIRepository(string? baseUrl = null, string? path = null, DelegatingHandler? handler = null)
        {
            // Ha nincs a paraméternek értéke, akkor automatikusan ezt vesz fel
            _baseUrl = baseUrl ?? "http://localhost:5000/";
            _path = path;
            _handler = handler;

            client = handler == null ? new HttpClient() : new HttpClient(handler);
            client.BaseAddress = new Uri(_baseUrl);
            // client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
