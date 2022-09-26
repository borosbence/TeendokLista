using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TeendokLista.MAUI.Repositories.API
{
    public class GenericAPIRepository<T> : IGenericRepository<T>
    {
        private readonly HttpClient client;
        private const string BASE_URL = "http://localhost:5000/";
        // private const string BASE_URL = "http://192.168.1.11:5000/";
        private const string PATH = "api/feladatok";

        public GenericAPIRepository()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL),
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<T>> GetAllAsync()
        {
            // TODO: hibakezelés
            return await client.GetFromJsonAsync<List<T>>(PATH);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<T>(PATH + "/" + id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(PATH + "/" + id);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task InsertAsync(T entity)
        {
            await client.PostAsJsonAsync(PATH, entity);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await client.PutAsJsonAsync(PATH + "/" + id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await client.DeleteAsync(PATH + "/" + id);
        }
    }
}
