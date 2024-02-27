using System.Net.Http.Headers;

namespace ApiClient.Repositories
{
    public class BaseAPIRepository
    {
        protected HttpClient client;
        protected DelegatingHandler? _handler;
        protected string _baseUrl;
        protected string? _path;

        public BaseAPIRepository(string? path = null, string? baseUrl = null, DelegatingHandler? handler = null)
        {
            _path = path;
            _baseUrl = baseUrl ?? "http://localhost:5000/";
            _handler = handler;
            client = handler == null ? new HttpClient() : new HttpClient(handler);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
