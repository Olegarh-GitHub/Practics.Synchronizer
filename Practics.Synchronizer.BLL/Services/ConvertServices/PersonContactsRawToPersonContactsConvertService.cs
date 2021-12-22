using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.DAL.Models;

namespace Practics.Synchronizer.BLL.Services.ConvertServices
{
    public class PersonContactsRawToPersonContactsConvertService : IRawEntityToImportedEntityConvertService<PersonContactsRaw, PersonContacts>
    {
        private readonly ImportedEntityService<Person> _personService;
        private readonly IMapper _mapper;

        public PersonContactsRawToPersonContactsConvertService
        (
            ImportedEntityService<Person> personService,
            IMapper mapper
        )
        {
            _personService = personService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonContacts>> Convert(IEnumerable<PersonContactsRaw> raws)
        {
            raws = raws.ToList();

            var personGuids = raws.Select(x => x.PersonGuid);
            var personIds = await _personService.ConvertExtKeyToIdAsync(personGuids);

            var personsContacts = new List<PersonContacts>();

            foreach (var personContactsRaw in raws)
            {
                if (personContactsRaw.PersonGuid == default)
                    continue;

                var personId = personIds.Convert(personContactsRaw.PersonGuid);

                if (personId == default)
                    continue;

                var personContacts = _mapper.Map<PersonContacts>(personContactsRaw);

                personContacts.PersonId = (int)personId;

                personsContacts.Add(personContacts);
            }

            return personsContacts;
        }
    }
}