using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.Local
{
    public class FeladatLocalRepository : IGenericRepository<Feladat>
    {
        private List<Feladat> feladatok;
        public FeladatLocalRepository()
        {
            feladatok = new List<Feladat>();
            feladatok.Add(new Feladat { Id = 1, Cim = "1. feladat", Tartalom = "Első", Teljesitve = true, FelhasznaloId = 1 });
            feladatok.Add(new Feladat
            {
                Id = 2,
                Cim = "2. feladat",
                Tartalom = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                FelhasznaloId = 1
            });
        }

        public Task<List<Feladat>> GetAllAsync()
        {
            // TODO: Teljesitve == true rendezésnél, de miért?
            var result = feladatok.OrderBy(x => x.Teljesitve).ThenBy(x => x.Hatarido).ToList();
            return Task.FromResult(result);
        }
        public Task<Feladat> GetByIdAsync(int id)
        {
            var result = feladatok.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(result);
        }
        public Task<bool> ExistsAsync(int id)
        {
            var result = feladatok.Any(x => x.Id == id);
            return Task.FromResult(result);
        }
        public Task InsertAsync(Feladat entity)
        {
            feladatok.Add(entity);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(int id, Feladat entity)
        {
            int index = feladatok.FindIndex(x => x.Id == id);
            feladatok[index] = entity;
            return Task.CompletedTask;
        }
        public Task DeleteAsync(int id)
        {
            var result = feladatok.FirstOrDefault(x => x.Id == id);
            feladatok.Remove(result);
            return Task.CompletedTask;
        }
    }
}
