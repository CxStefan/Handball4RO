using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Services
{
    public class StireService : IStireService
    {
        
        private readonly IGenericRepository<Stire> _repository;

        public StireService(IGenericRepository<Stire> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Stire>> ObtineToateStirileAsync() => await _repository.GetAllAsync();

        public async Task<Stire> ObtineStireDupaIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AdaugaStireAsync(Stire stire)
        {
            stire.DataPublicare = DateTime.Now; 
            await _repository.AddAsync(stire);
        }

        public async Task EditeazaStireAsync(Stire stire) => await _repository.UpdateAsync(stire);

        public async Task StergeStireAsync(int id)
        {
            var stire = await _repository.GetByIdAsync(id);
            if (stire != null)
            {
                await _repository.DeleteAsync(stire);
            }
        }
    }
}