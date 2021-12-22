using System;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.DAL.Models
{
    public class WorkerRaw : RawEntity
    {
        public string WorkerNumber { get; set; }
        private readonly byte[] _departmentId;
        public string DepartmentGuid => _departmentId.BytesToGuid();

        private readonly byte[] _personId;
        public string PersonGuid => _personId.BytesToGuid();

        private readonly byte[] _chiefId;
        public string ChiefGuid => _chiefId.BytesToGuid();

        private readonly byte[] _awardStatusId;
        public string AwardStatusGuid => _awardStatusId.BytesToGuid();

        private readonly byte[] _fired;
        public bool Fired => BitConverter.ToBoolean(_fired);

        private readonly byte[] _maternityLeave;
        public bool MaternityLeave => BitConverter.ToBoolean(_maternityLeave);
        
        public WorkerRaw
        (
            byte[] id,
            string workerNumber,
            byte[] departmentId,
            byte[] personId,
            byte[] chiefId,
            byte[] awardStatusId,
            byte[] fired,
            byte[] maternityLeave,
            int hash
        ) : base(id, hash)
        {
            WorkerNumber = workerNumber;
            _departmentId = departmentId;
            _personId = personId;
            _chiefId = chiefId;
            _awardStatusId = awardStatusId;
            _fired = fired;
            _maternityLeave = maternityLeave;
        }
    }
}