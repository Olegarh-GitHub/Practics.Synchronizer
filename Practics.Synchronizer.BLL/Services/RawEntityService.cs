using System.Collections.Generic;
using System.Threading.Tasks;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.BLL.Services
{
    public class RawEntityService<TRaw>
    where TRaw : RawEntity
    {
        private readonly IRepository<TRaw, string> _repository;

        public RawEntityService(IRepository<TRaw, string> repository)
        {
            _repository = repository;
        }

        public async Task<TRaw> GetAsync(string guid)
        {
            return await _repository.GetAsync(guid);
        }

        public async Task<List<TRaw>> GetAsync(List<string> guids)
        {
            return await _repository.GetAsync(guids);
        }

        public async Task<IEnumerable<TRaw>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}