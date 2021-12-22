using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Extensions
{
    public static class ImportedEntityExtensions
    {
        public static (bool created, bool updated, bool deleted) GetEntityFlags(this ImportedEntity entity)
        {
            return (entity.MarkedForCreate, entity.MarkedForUpdate, entity.MarkedForDelete);
        }

        public static void SetEntityFlags(this ImportedEntity entity, (bool created, bool updated, bool deleted) flags)
        {
            var (created, updated, deleted) = flags;
            entity.MarkedForCreate = created;
            entity.MarkedForUpdate = updated;
            entity.MarkedForDelete = deleted;
        }
    }
}