using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using AutoMapper;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.DAL.Models;

namespace Practics.Synchronizer.BLL.MappingProfiles
{
    public class RawEntityToImportedEntityProfile : Profile
    {
        public RawEntityToImportedEntityProfile()
        {
            CreateMap<PersonContactsRaw, PersonContacts>()
                .ForMember(x => x.ExtKey, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());

            
            CreateMap<PersonRaw, Person>()
                .ForMember(x => x.ExtKey, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<DepartmentRaw, Department>()
                .ForMember(x => x.ExtKey, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<WorkerRaw, Worker>()
                .ForMember(x => x.ExtKey, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<AwardStatusRaw, AwardStatus>()
                .ForMember(x => x.ExtKey, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}