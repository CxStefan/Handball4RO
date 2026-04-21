using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Services
{
    public class CompetitieService : ICompetitieService
    {
        private readonly IGenericRepository<Competitie> _competitieRepository;
        private readonly IGenericRepository<Clasament> _clasamentRepository;

        public CompetitieService(
            IGenericRepository<Competitie> competitieRepository,
            IGenericRepository<Clasament> clasamentRepository)
        {
            _competitieRepository = competitieRepository;
            _clasamentRepository = clasamentRepository;
        }

        public async Task<IEnumerable<Competitie>> ObtineToateCompetitiileAsync()
        {
            return await _competitieRepository.GetAllAsync();
        }

        public async Task<Competitie> ObtineCompetitieDupaIdAsync(int id)
        {
            return await _competitieRepository.GetByIdAsync(id);
        }

        public async Task AdaugaCompetitieAsync(Competitie competitie)
        {
            await _competitieRepository.AddAsync(competitie);
        }

        public async Task EditeazaCompetitieAsync(Competitie competitie)
        {
            await _competitieRepository.UpdateAsync(competitie);
        }

        public async Task StergeCompetitieAsync(int id)
        {
            var competitie = await _competitieRepository.GetByIdAsync(id);
            if (competitie != null)
            {
                await _competitieRepository.DeleteAsync(competitie);
            }
        }

        public async Task<IEnumerable<Clasament>> ObtineClasamentAsync(int competitieId)
        {
            var totClasamentul = await _clasamentRepository.GetAllAsync("Echipa");

            return totClasamentul
                .Where(c => c.CompetitieId == competitieId)
                .OrderByDescending(c => c.Puncte)
                .ThenByDescending(c => (c.GoluriMarcate - c.GoluriPrimite))
                .ToList();
        }
    }
}