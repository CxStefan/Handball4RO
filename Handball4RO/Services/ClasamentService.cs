using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Services
{
    public class ClasamentService : IClasamentService
    {
        private readonly IGenericRepository<Clasament> _repo;

        public ClasamentService(IGenericRepository<Clasament> repo) => _repo = repo;

        public async Task<Clasament> ObtineDupaIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task AdaugaInClasamentAsync(Clasament clasament) => await _repo.AddAsync(clasament);

        public async Task ActualizeazaDateAsync(Clasament clasament) => await _repo.UpdateAsync(clasament);

        public async Task StergeDinClasamentAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item != null) await _repo.DeleteAsync(item);
        }
    }
}