using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FeladatAPIRepository : IGenericRepository<Feladat>
    {
        public Task<List<Feladat>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Feladat> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Feladat entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Feladat entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
