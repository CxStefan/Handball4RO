using Handball4RO.Models;
using Handball4RO.Repositories;
using Handball4RO.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EchipaService : IEchipaService
{
    private readonly IGenericRepository<Echipa> _repo;

    private readonly IGenericRepository<Clasament> _clasamentRepo;

    public EchipaService(IGenericRepository<Echipa> repo, IGenericRepository<Clasament> clasamentRepo)
    {
        _repo = repo;
        _clasamentRepo = clasamentRepo;
    }

    public async Task<IEnumerable<Echipa>> ObtineToateAsync() => await _repo.GetAllAsync();

    public async Task<Echipa> ObtineDupaIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task AdaugaAsync(Echipa echipa, int? competitieId = null)
    {
        await _repo.AddAsync(echipa);

        if (competitieId.HasValue && echipa.Id > 0)
        {
            var intrareClasament = new Clasament
            {
                EchipaId = echipa.Id,
                CompetitieId = competitieId.Value,
                MeciuriJucate = 0,
                Victorii = 0,
                Egaluri = 0,
                Infrangeri = 0,
                GoluriMarcate = 0,
                GoluriPrimite = 0,
                Puncte = 0
            };

            await _clasamentRepo.AddAsync(intrareClasament);
        }
    }

    public async Task EditeazaAsync(Echipa echipa) => await _repo.UpdateAsync(echipa);

    public async Task StergeAsync(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e != null) await _repo.DeleteAsync(e);
    }

    public async Task<IEnumerable<Echipa>> ObtineEchipeDupaCompetitieAsync(int competitieId)
    {
        var toateClasamentele = await _clasamentRepo.GetAllAsync();

        var idUriEchipeInCompetitie = toateClasamentele
            .Where(c => c.CompetitieId == competitieId)
            .Select(c => c.EchipaId)
            .ToList();

        var toateEchipele = await _repo.GetAllAsync();

        return toateEchipele.Where(e => idUriEchipeInCompetitie.Contains(e.Id)).ToList();
    }
}