using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Services
{
    public class JucatorService : IJucatorService
    {
        private readonly IGenericRepository<Jucator> _repo;

        public JucatorService(IGenericRepository<Jucator> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Jucator>> ObtineJucatoriDupaEchipaAsync(int echipaId)
        {
            var totiJucatorii = await _repo.GetAllAsync();

            return totiJucatorii.Where(j => j.EchipaId == echipaId).ToList();
        }

        public async Task<Jucator> ObtineDupaIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AdaugaAsync(Jucator jucator)
        {
            await _repo.AddAsync(jucator);
        }

        public async Task EditeazaAsync(Jucator jucator)
        {
            await _repo.UpdateAsync(jucator);
        }

        public async Task StergeAsync(int id)
        {
            var jucator = await _repo.GetByIdAsync(id);
            if (jucator != null)
            {
                await _repo.DeleteAsync(jucator);
            }
        }
    }
}