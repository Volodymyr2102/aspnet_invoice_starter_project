using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Definuje smlouvu pro správu osob (Person).
    /// Rozhraní zajišťuje metody pro čtení, vytváření a mazání osob.
    /// Implementace obsahuje samotnou byznys logiku.
    /// </summary>
    public interface IPersonManager
    {
        /// <summary>
        /// Vrátí seznam všech osob uložených v systému.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonDto"/> reprezentující všechny osoby.</returns>
        IEnumerable<PersonDto> GetAllPersons();

        /// <summary>
        /// Vratí detali osoby
        /// </summary>
        /// <param name="id">Id osoby</param>
        /// <returns>Vrácená osoba</returns>
        PersonDto GetPersonById(int id);

        /// <summary>
        /// Vytvoří novou osobu na základě dodaného datového objektu.
        /// </summary>
        /// <param name="dto">Objekt <see cref="PersonDto"/> s daty nové osoby.</param>
        /// <returns>Nově vytvořená osoba ve formě <see cref="PersonDto"/>.</returns>
        PersonDto AddPerson(PersonDto dto);

        /// <summary>
        /// Smaže osobu podle jejího ID.
        /// </summary>
        /// <param name="id">Jedinečné ID osoby, která má být odstraněna.</param>
        /// <returns>
        /// <c>true</c>, pokud byla osoba úspěšně smazána, 
        /// nebo <c>false</c>, pokud osoba s daným ID neexistuje.
        /// </returns>
        bool DeletePerson(int id);

        /// <summary>
        /// Aktualizuje existující osobu podle jejího ID.
        /// </summary>
        /// <param name="id">ID osoby, která má být aktualizována.</param>
        /// <param name="dto">Objekt <see cref="PersonDto"/> s novými hodnotami.</param>
        /// <returns>
        /// Aktualizovaná osoba ve formě <see cref="PersonDto"/>, 
        /// nebo <c>null</c>, pokud osoba s daným ID neexistuje.
        /// </returns>
        PersonDto? UpdatePerson(int id, PersonDto dto);
    }
}
