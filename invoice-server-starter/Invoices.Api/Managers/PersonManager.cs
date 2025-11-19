using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manager vrstva obsahuje aplikační logiku spojenou s entitou Person.
    /// Odděluje controller od přístupu k datům a umožňuje snadnější testování a údržbu.
    /// </summary>
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public IEnumerable<PersonDto> GetAllPersons()
        {
            // Vrátíme pouze aktivní (neskryté) osoby
            IEnumerable<Person> people = personRepository.GetByHidden(false);
            return mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public PersonDto AddPerson(PersonDto dto)
        {
            Person person = mapper.Map<Person>(dto);
            Person addedPerson = personRepository.Add(person);
            personRepository.SaveChanges();
            return mapper.Map<PersonDto>(addedPerson);
        }

        public bool DeletePerson(int id)
        {
            // Soft delete: nastavíme příznak Hidden = true místo fyzického smazání
            if (HidePerson(id) is not null)
            {
                personRepository.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Označí osobu jako skrytou (soft delete).
        /// Pokud osoba neexistuje, vrací null.
        /// </summary>
        private Person? HidePerson(int personId)
        {
            Person? person = personRepository.FindById(personId);

            if (person is null)
                return null;

            person.Hidden = true;
            return personRepository.Update(person);
        }
        public PersonDto GetPersonById(int id)
        {
            Person? person = personRepository.FindById(id);
            if (person is null)
            {
                return null;
            }
            return mapper.Map<PersonDto>(person);
        }
        public PersonDto? UpdatePerson(int id, PersonDto dto)
        {
            // Najdeme existující osobu podle ID
            var existing = personRepository.FindById(id);
            if (existing is null)
                return null;

            // Přemapujeme hodnoty z DTO do entity
            // (PersonId neměníme – ID vždy přichází z URL)
            mapper.Map(dto, existing);
            existing.PersonId = id;

            // Uložíme změny do databáze
            var updated = personRepository.Update(existing);
            personRepository.SaveChanges();

            // Vrátíme aktualizovaný DTO objekt
            return mapper.Map<PersonDto>(updated);
        }
    }
}
