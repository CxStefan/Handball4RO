using Handball4RO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handball4RO.Services
{
    public interface IMeciService
    {
        
        Task<IEnumerable<Meci>> ObtineMeciuriDupaCompetitieAsync(int competitieId);

        Task<Meci> ObtineDupaIdAsync(int id);

        Task AdaugaAsync(Meci meci);

        Task EditeazaAsync(Meci meci);

        Task StergeAsync(int id);
    }
}