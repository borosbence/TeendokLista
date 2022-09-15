using System.Net.Http.Headers;
using System.Net.Http.Json;
using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FeladatAPIRepository : IGenericRepository<Feladat>
    {
        private readonly HttpClient client;
        private const string BASE_URL = "http://localhost:5000/";
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
            HttpResponseMessage response = await client.GetAsync(PATH);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Feladat>>();
            }
            return null;
        }

        public async Task<Feladat> GetByIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync(PATH + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Feladat>();
            }
            return null;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync(PATH + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var feladat = await response.Content.ReadFromJsonAsync<Feladat>();
                return feladat != null;
            }
            return false;
        }

        public async Task InsertAsync(Feladat entity)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(PATH, entity);
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task UpdateAsync(Feladat entity)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(PATH + "/" + entity.Id, entity);
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        public async Task DeleteAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(PATH + "/" + id);
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
    }
}
