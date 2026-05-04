using Handball4RO.Models;

namespace Handball4RO.Services
{
    public interface IJucatorService
    {
        Task<IEnumerable<Jucator>> ObtineJucatoriDupaEchipaAsync(int echipaId);
        Task<Jucator> ObtineDupaIdAsync(int id);
        Task AdaugaAsync(Jucator jucator);
        Task EditeazaAsync(Jucator jucator);
        Task StergeAsync(int id);
    }
}