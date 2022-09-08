using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories
{
    public class FeladatLocalRepository : IGenericRepository<Feladat>
    {
        private List<Feladat> Feladatok;
        public FeladatLocalRepository()
        {
            Feladatok = new List<Feladat>();
            Feladatok.Add(new Feladat { id = 1, cim = "1. feladat", tartalom = "Első", teljesitve = true });
            Feladatok.Add(new Feladat { id = 2, cim = "2. feladat", tartalom = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.
Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.
Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.
Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.
Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.
" });
        }

        public Task<List<Feladat>> GetAllAsync()
        {
            var result = Feladatok.OrderByDescending(x => x.hatarido).ToList();
            return Task.FromResult(result);
        }
        public Task<Feladat> GetByIdAsync(int id)
        {
            var result = Feladatok.FirstOrDefault(x => x.id == id);
            return Task.FromResult(result);
        }
        public Task<bool> ExistsAsync(int id)
        {
            var result = Feladatok.Any(x => x.id == id);
            return Task.FromResult(result);
        }
        public Task InsertAsync(Feladat entity)
        {
            Feladatok.Add(entity);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Feladat entity)
        {
            int index = Feladatok.FindIndex(x => x.id == entity.id);
            Feladatok[index] = entity;
            return Task.CompletedTask;
        }
        public Task DeleteAsync(int id)
        {
            var result = Feladatok.FirstOrDefault(x => x.id == id);
            Feladatok.Remove(result);
            return Task.CompletedTask;
        }
    }
}
