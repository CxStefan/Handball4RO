using Handball4RO.Models;
using Handball4RO.Repositories;

public class EchipaService : IEchipaService
{
    private readonly IGenericRepository<Echipa> _repo;
    public EchipaService(IGenericRepository<Echipa> repo) => _repo = repo;
    public async Task<IEnumerable<Echipa>> ObtineToateAsync() => await _repo.GetAllAsync();
    public async Task<Echipa> ObtineDupaIdAsync(int id) => await _repo.GetByIdAsync(id);
    public async Task AdaugaAsync(Echipa echipa) => await _repo.AddAsync(echipa);
    public async Task EditeazaAsync(Echipa echipa) => await _repo.UpdateAsync(echipa);
    public async Task StergeAsync(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e != null) await _repo.DeleteAsync(e);
    }
}