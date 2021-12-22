using AutoMapper;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.Core.Models.DTO;

namespace Practics.Synchronizer.BLL.MappingProfiles
{
    public class ImportedEntityToDTOProfile : Profile
    {
        public ImportedEntityToDTOProfile()
        {
            CreateMap<Worker, WorkerDTO>()
                .ReverseMap();

            CreateMap<Person, PersonDTO>()
                .ReverseMap();
            
            CreateMap<PersonContacts, PersonContactsDTO>()
                .ReverseMap();
            
            CreateMap<Department, DepartmentDTO>()
                .ReverseMap();
            
            CreateMap<AwardStatus, AwardStatusDTO>()
                .ReverseMap();
        }
    }
}