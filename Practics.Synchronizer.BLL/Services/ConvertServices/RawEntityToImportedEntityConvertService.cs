using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.BLL.Services.ConvertServices
{
    public class RawEntityToImportedEntityConvertService<TRaw, TEntity> 
        : IRawEntityToImportedEntityConvertService<TRaw, TEntity>
        where TRaw : class
        where TEntity : ImportedEntity
    {
        private readonly IMapper _mapper;

        public RawEntityToImportedEntityConvertService(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public TEntity Convert(TRaw raw)
        {
            return _mapper.Map<TEntity>(raw);
        }
        
        public async Task<IEnumerable<TEntity>> Convert(IEnumerable<TRaw> raws)
        {
            var entities = new List<TEntity>();

            foreach (var raw in raws)
            {
                var entity = _mapper.Map<TEntity>(raw);

                entities.Add(entity);
            }

            return entities;
        }
    }
}