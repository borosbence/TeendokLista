using ApiClient.Repositories;
using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.Local
{
    public class FeladatLocalRepository : IGenericRepository<Feladat>
    {
        private List<Feladat> _feladatok;
        private int _felhasznaloId;

        public FeladatLocalRepository(CurrentUser currentUser)
        {
            _felhasznaloId = currentUser.Id;
            _feladatok = new List<Feladat>
            {
                new Feladat(_felhasznaloId) { Id = 1, Cim = "1. feladat", Tartalom = "Első", Teljesitve = true },
                new Feladat(_felhasznaloId)
                {
                    Id = 2,
                    Cim = "2. feladat",
                    Tartalom = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                }
            };
        }

        public Task<List<Feladat>?> GetAllAsync()
        {
            var result = _feladatok.OrderBy(x => x.Teljesitve).ThenBy(x => x.Hatarido).ToList();
            return Task.FromResult(result);
        }
        public async Task<Feladat?> GetByIdAsync(int id)
        {
            var result = _feladatok.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(result);
        }
        public async Task<bool> ExistsByIdAsync(int id)
        {
            var result = _feladatok.Any(x => x.Id == id);
            return await Task.FromResult(result);
        }
        public async Task InsertAsync(Feladat entity)
        {
            _feladatok.Add(entity);
            // return Task.CompletedTask;
            return;
        }
        public async Task UpdateAsync(int id, Feladat entity)
        {
            var feladat = _feladatok.FirstOrDefault(x => x.Id == id);
            feladat = entity;
            // feladatok[index] = entity;
            // return Task.CompletedTask;
            return;
        }
        public async Task DeleteAsync(int id)
        {
            var result = _feladatok.FirstOrDefault(x => x.Id == id);
            _feladatok.Remove(result);
            // return Task.CompletedTask;
            return;
        }
    }
}
