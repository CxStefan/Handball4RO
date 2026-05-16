using Handball4RO.Models;

namespace Handball4RO.Services
{
    public interface IStireService
    {
        Task<IEnumerable<Stire>> ObtineToateStirileAsync();
        Task<Stire> ObtineStireDupaIdAsync(int id);
        Task AdaugaStireAsync(Stire stire);
        Task EditeazaStireAsync(Stire stire);
        Task StergeStireAsync(int id);
    }
}