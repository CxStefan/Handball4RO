using Handball4RO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handball4RO.Services
{
    public interface IEchipaService
    {
        Task<IEnumerable<Echipa>> ObtineToateAsync();
        Task<Echipa> ObtineDupaIdAsync(int id);

        Task AdaugaAsync(Echipa echipa, int? competitieId = null);

        Task EditeazaAsync(Echipa echipa);
        Task StergeAsync(int id);
        Task<IEnumerable<Echipa>> ObtineEchipeDupaCompetitieAsync(int competitieId);
    }
}