using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.BLL.Services
{
    public class ImportedEntityUpdateService<TRaw, TEntity>
    where TEntity : ImportedEntity
    where TRaw : RawEntity
    {
        private readonly IMapper _mapper;
        
        private readonly IRawEntityToImportedEntityConvertService<TRaw, TEntity> _converter;
        private readonly RawEntityService<TRaw> _rawEntityService;
        private readonly ImportedEntityService<TEntity> _importedEntityService;
        
        public ImportedEntityUpdateService(
            IMapper mapper,
            RawEntityService<TRaw> rawEntityService,
            IRawEntityToImportedEntityConvertService<TRaw, TEntity> converter,
            ImportedEntityService<TEntity> importedEntityService)
        {
            _mapper = mapper;
            
            _rawEntityService = rawEntityService;
            _converter = converter;
            _importedEntityService = importedEntityService;
        }

        public async Task<(int, int, int)> Update()
        {
            var news = await _rawEntityService.GetAllAsync();
            news = news.DistinctBy(x => x.Id).ToList();
            var newGuids = news.Select(x => x.Id).ToList();

            var olds = await _importedEntityService.GetAllAsync();
            var oldHashes = olds.Select(x => x.Hash).ToList();
            
            var created = 0;
            var updated = 0;
            var deleted = 0;

            var lostOnes = olds.Where(x => !newGuids.Contains(x.ExtKey)).ToList();
            
            foreach (var lost in lostOnes)
            {
                lost.MarkedForDelete = true;
                deleted++;
            }

            await _importedEntityService.UpdateAsync(lostOnes);

            var newOnesRaw = news.Where(x => !oldHashes.Contains(x.Hash)).ToList();

            var newOnes = await _converter.Convert(newOnesRaw);
            
            foreach (var newOne in newOnes)
            {
                var oldOne = olds.FirstOrDefault(x => x.ExtKey == newOne.ExtKey);

                if (oldOne == null)
                {
                    newOne.MarkedForCreate = true;
                    
                    await _importedEntityService.CreateAsync(newOne);

                    created++;
                }
                else
                {
                    var oldOneFlags = oldOne.GetEntityFlags();
                    
                    _mapper.Map(newOne, oldOne);

                    oldOne.SetEntityFlags(oldOneFlags);
                    
                    if (!oldOne.MarkedForCreate)
                        oldOne.MarkedForUpdate = true;

                    await _importedEntityService.UpdateAsync(oldOne);
                    
                    updated++;
                }
            }

            return (created, updated, deleted);
        }
    }
}