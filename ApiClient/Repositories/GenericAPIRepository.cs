using System.Net.Http.Json;

namespace ApiClient.Repositories
{
    public class GenericAPIRepository<T> : BaseAPIRepository, IGenericRepository<T>
    {
        public GenericAPIRepository(string path, string? baseUrl = null, DelegatingHandler? handler = null) : base(baseUrl, path, handler)
        {
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await client.GetFromJsonAsync<List<T>>(_path);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<T>(_path + "/" + id);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(_path + "/" + id);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task InsertAsync(T entity)
        {
            await client.PostAsJsonAsync(_path, entity);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await client.PutAsJsonAsync(_path + "/" + id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await client.DeleteAsync(_path + "/" + id);
        }
    }
}
