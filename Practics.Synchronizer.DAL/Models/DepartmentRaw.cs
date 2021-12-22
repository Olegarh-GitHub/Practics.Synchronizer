using System;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.DAL.Models
{
    public class DepartmentRaw : RawEntity
    {
        public string Number { get; set; }
        public string Name { get; set; }
        
        private readonly byte[] _disabled;
        public bool Disabled => BitConverter.ToBoolean(_disabled);
        
        
        public DepartmentRaw
        (
            byte[] id,
            string number,
            string name,
            byte[] disabled,
            int hash
        ) : base(id, hash)
        {
            Number = number;
            Name = name;
            _disabled = disabled;
        }
    }
}