using System.Collections.Generic;
using System.Threading.Tasks;
using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Interfaces
{
    public interface IRawEntityToImportedEntityConvertService<TRaw, TEntity>
        where TRaw : class
        where TEntity : ImportedEntity
    {
        public Task<IEnumerable<TEntity>> Convert(IEnumerable<TRaw> raws);
    }
}