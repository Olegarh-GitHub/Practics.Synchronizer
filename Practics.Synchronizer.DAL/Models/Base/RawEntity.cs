using System;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.Core.Interfaces;

namespace Practics.Synchronizer.DAL.Models.Base
{
    public abstract class RawEntity : IEntity<string>
    {
        private readonly byte[] _id;
        public virtual string Id
        {
            get => _id.BytesToGuid();
            set {}
        }

        public int Hash { get; set; }

        public RawEntity(byte[] id, int hash)
        {
            _id = id;
            Hash = hash;
        }
    }
}