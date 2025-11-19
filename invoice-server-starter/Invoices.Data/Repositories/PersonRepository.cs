using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(InvoicesDbContext context) : base(context) { }

        public IEnumerable<Person> GetByHidden(bool hidden)
        {
            return dbSet.Where(x => x.Hidden == hidden).ToList();
        }

    }
}