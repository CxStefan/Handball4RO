using Handball4RO.Models;

public interface IEchipaService
{
    Task<IEnumerable<Echipa>> ObtineToateAsync();
    Task<Echipa> ObtineDupaIdAsync(int id);
    Task AdaugaAsync(Echipa echipa);
    Task EditeazaAsync(Echipa echipa);
    Task StergeAsync(int id);
}