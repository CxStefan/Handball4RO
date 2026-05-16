using Handball4RO.Models;
using Handball4RO.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handball4RO.Services
{
    public class MeciService : IMeciService
    {
        private readonly IGenericRepository<Meci> _repo;

        public MeciService(IGenericRepository<Meci> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Meci>> ObtineMeciuriDupaCompetitieAsync(int competitieId)
        {
            var toateMeciurile = await _repo.GetAllAsync();


            return toateMeciurile
                .Where(m => m.CompetitieId == competitieId)
                .OrderBy(m => m.DataMeci)
                .ToList();
        }

        public async Task<Meci> ObtineDupaIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AdaugaAsync(Meci meci)
        {
            await _repo.AddAsync(meci);
        }

        public async Task EditeazaAsync(Meci meci)
        {
            await _repo.UpdateAsync(meci);
        }

        public async Task StergeAsync(int id)
        {
            var meci = await _repo.GetByIdAsync(id);
            if (meci != null)
            {
                await _repo.DeleteAsync(meci);
            }
        }
    }
}