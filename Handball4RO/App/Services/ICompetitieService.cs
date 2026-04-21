using Handball4RO.Models;

namespace Handball4RO.Services
{
    public interface ICompetitieService
    {
        Task<IEnumerable<Competitie>> ObtineToateCompetitiileAsync();
        Task<Competitie> ObtineCompetitieDupaIdAsync(int id);
        Task AdaugaCompetitieAsync(Competitie competitie);
        Task EditeazaCompetitieAsync(Competitie competitie);
        Task StergeCompetitieAsync(int id);
        Task<IEnumerable<Clasament>> ObtineClasamentAsync(int competitieId);
    }
}