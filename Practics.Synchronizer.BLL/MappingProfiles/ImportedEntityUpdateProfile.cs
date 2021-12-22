using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using AutoMapper;
using Practics.Synchronizer.Core.Models;

namespace Practics.Synchronizer.BLL.MappingProfiles
{
    public class ImportedEntityUpdateProfile : Profile
    {
        public ImportedEntityUpdateProfile()
        {
            CreateMap<Worker, Worker>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Department, opt => opt.Ignore())
                .ForMember(x => x.Chief, opt => opt.Ignore())
                .ForMember(x=>x.AwardStatus, opt => opt.Ignore());

            CreateMap<Department, Department>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Person, Person>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<AwardStatus, AwardStatus>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<PersonContacts, PersonContacts>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Person, opt => opt.Ignore());
        }
    }
}