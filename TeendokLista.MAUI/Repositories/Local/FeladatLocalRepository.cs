using ApiClient.Repositories;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Repositories.Local
{
    public class FeladatLocalRepository : IGenericRepository<FeladatModel>
    {
        private List<FeladatModel> _feladatok;

        public FeladatLocalRepository()
        {
            _feladatok = new List<FeladatModel>
            {
                new FeladatModel() { Id = 1, Cim = "1. feladat", Tartalom = "Első", Teljesitve = true, FelhasznaloId = CurrentUser.Id },
                new FeladatModel()
                {
                    Id = 2,
                    Cim = "2. feladat",
                    Tartalom = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    FelhasznaloId = CurrentUser.Id
                }
            };
        }

        public async Task<List<FeladatModel>?> GetAllAsync()
        {
            var result = _feladatok.OrderBy(x => x.Teljesitve).ThenBy(x => x.Hatarido).ToList();
            return await Task.FromResult(result);
        }
        public async Task<FeladatModel?> GetByIdAsync(int id)
        {
            var result = _feladatok.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(result);
        }
        public async Task<bool> ExistsByIdAsync(int id)
        {
            var result = _feladatok.Any(x => x.Id == id);
            return await Task.FromResult(result);
        }
        public async Task InsertAsync(FeladatModel entity)
        {
            // ID növelése
            var maxId = _feladatok.Max(x => x.Id);
            entity.Id = maxId + 1;
            _feladatok.Add(entity);
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(int id, FeladatModel entity)
        {
            var feladat = _feladatok.FirstOrDefault(x => x.Id == id);
            if (feladat != null)
            {
                feladat = entity;
            }
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(int id)
        {
            var result = _feladatok.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                _feladatok.Remove(result);
            }
            await Task.CompletedTask;
        }
    }
}
