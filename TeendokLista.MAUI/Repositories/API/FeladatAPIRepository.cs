using System.Net.Http.Headers;
using System.Net.Http.Json;
using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FeladatAPIRepository : IGenericRepository<Feladat>
    {
        private readonly HttpClient client;
        //private const string BASE_URL = "http://localhost:5000/";
        private const string BASE_URL = "http://192.168.1.11:5000/";
        private const string PATH = "api/feladatok";

        public FeladatAPIRepository()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Feladat>> GetAllAsync()
        {
            return await client.GetFromJsonAsync<List<Feladat>>(PATH);
        }

        public async Task<Feladat> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<Feladat>(PATH + "/" + id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(PATH + "/" + id);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task InsertAsync(Feladat entity)
        {
            await client.PostAsJsonAsync(PATH, entity);
        }

        public async Task UpdateAsync(Feladat entity)
        {
            await client.PutAsJsonAsync(PATH + "/" + entity.Id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await client.DeleteAsync(PATH + "/" + id);
        }
    }
}
