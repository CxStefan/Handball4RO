using Handball4RO.Models;

namespace Handball4RO.Services
{
    public interface IClasamentService
    {
        Task<Clasament> ObtineDupaIdAsync(int id);
        Task AdaugaInClasamentAsync(Clasament clasament);
        Task ActualizeazaDateAsync(Clasament clasament);
        Task StergeDinClasamentAsync(int id);
    }
}