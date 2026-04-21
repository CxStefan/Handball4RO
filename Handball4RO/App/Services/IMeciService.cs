using Handball4RO.Models;

public interface IMeciService
{
    Task<IEnumerable<Meci>> ObtineMeciuriCompetitieAsync(int competitieId);
    Task AdaugaMeciAsync(Meci meci);
    Task ActualizeazaScorAsync(int meciId, int scorG, int scorO);
}