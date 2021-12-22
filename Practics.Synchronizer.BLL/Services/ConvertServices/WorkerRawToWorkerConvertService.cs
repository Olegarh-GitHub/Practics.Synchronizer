using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.DAL.Models;

namespace Practics.Synchronizer.BLL.Services.ConvertServices
{
    public class WorkerRawToWorkerConvertService: IRawEntityToImportedEntityConvertService<WorkerRaw, Worker>
    {
        private readonly IMapper _mapper;

        private readonly ImportedEntityService<Department> _departmentService;
        private readonly ImportedEntityService<Person> _personService;
        private readonly ImportedEntityService<Worker> _workerService;
        private readonly ImportedEntityService<AwardStatus> _awardStatusService;

        public WorkerRawToWorkerConvertService(
            IMapper mapper,
            ImportedEntityService<Department> departmentService,
            ImportedEntityService<Person> personService,
            ImportedEntityService<Worker> workerService, 
            ImportedEntityService<AwardStatus> awardStatusService)
        {
            _mapper = mapper;

            _departmentService = departmentService;
            _personService = personService;
            _workerService = workerService;
            _awardStatusService = awardStatusService;
        }
        
        public async Task<Worker> Convert(WorkerRaw workerRaw)
        {
            var worker = _mapper.Map<Worker>(workerRaw);

            var departmentId = await _departmentService.ConvertExtKeyToIdAsync(workerRaw.DepartmentGuid);
            
            if (departmentId == default)
                return null;
            
            worker.DepartmentId = departmentId;

            var personId  = await _personService.ConvertExtKeyToIdAsync(workerRaw.PersonGuid);
            
            if (personId == default)
                return null;
            
            worker.PersonId = personId;

            var chiefId = await _workerService.ConvertExtKeyToIdAsync(workerRaw.ChiefGuid);
            
            if (chiefId == default)
                return null;
            
            worker.ChiefId = chiefId;

            var awardStatusId = await _awardStatusService.ConvertExtKeyToIdAsync(workerRaw.AwardStatusGuid);

            if (awardStatusId == default)
                return null;
            
            worker.AwardStatusId = awardStatusId;
            
            return worker;
        }
        
        public async Task<IEnumerable<Worker>> Convert(IEnumerable<WorkerRaw> workersRaws)
        {
            workersRaws = workersRaws.ToList();
            
            var departmentsGuids = workersRaws.Select(x => x.DepartmentGuid);
            var departmentsIds = await _departmentService.ConvertExtKeyToIdAsync(departmentsGuids);
            
            var personsGuids = workersRaws.Select(x => x.PersonGuid);
            var personsIds = await _personService.ConvertExtKeyToIdAsync(personsGuids);

            var chiefsGuids = workersRaws.Select(x => x.ChiefGuid);
            var chiefsIds = await _workerService.ConvertExtKeyToIdAsync(chiefsGuids);

            var awardStatusGuids = workersRaws.Select(x => x.AwardStatusGuid);
            var awardStatusIds = await _awardStatusService.ConvertExtKeyToIdAsync(awardStatusGuids);
            
            var workers = new List<Worker>();

            foreach (var workerRaw in workersRaws)
            {
                if (workerRaw.DepartmentGuid == null && workerRaw.PersonGuid == null)
                    continue;
                
                var worker = _mapper.Map<Worker>(workerRaw);

                var departmentId = departmentsIds.Convert(workerRaw.DepartmentGuid);

                if (departmentId == default)
                    continue;

                worker.DepartmentId = (int)departmentId;

                var personId = personsIds.Convert(workerRaw.PersonGuid);

                if (personId == default)
                    continue;

                worker.PersonId = (int)personId;

                var chiefId = chiefsIds.Convert(workerRaw.ChiefGuid);

                if (workerRaw.ChiefGuid == "00000000-0000-0000-0000-000000000000")
                    worker.ChiefId = null;
                
                if (workerRaw.ChiefGuid != null && chiefId == default && workerRaw.ChiefGuid != "00000000-0000-0000-0000-000000000000")
                    continue;
                
                worker.ChiefId = chiefId;
                
                var awardStatusId = awardStatusIds.Convert(workerRaw.AwardStatusGuid);

                if (workerRaw.AwardStatusGuid == "00000000-0000-0000-0000-000000000000")
                    worker.AwardStatusId = null;
                
                if (workerRaw.AwardStatusGuid != null && awardStatusId == default && workerRaw.AwardStatusGuid != "00000000-0000-0000-0000-000000000000")
                    continue;
                
                worker.AwardStatusId = awardStatusId;
                
                workers.Add(worker);
            }
            
            return workers;
        }
    }
}