using Handball4RO.Models;
using Handball4RO.Repositories;

public class MeciService : IMeciService
{
    private readonly IGenericRepository<Meci> _repo;
    public MeciService(IGenericRepository<Meci> repo) => _repo = repo;

    public async Task<IEnumerable<Meci>> ObtineMeciuriCompetitieAsync(int competitieId)
    {
        var toate = await _repo.GetAllAsync();
        return toate.Where(m => m.CompetitieId == competitieId);
    }

    public async Task AdaugaMeciAsync(Meci meci) => await _repo.AddAsync(meci);

    public async Task ActualizeazaScorAsync(int meciId, int scorG, int scorO)
    {
        var meci = await _repo.GetByIdAsync(meciId);
        if (meci != null)
        {
            meci.ScorGazda = scorG;
            meci.ScorOaspete = scorO;
            await _repo.UpdateAsync(meci);
        }
    }
}