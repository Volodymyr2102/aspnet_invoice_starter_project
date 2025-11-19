using Invoices.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        IEnumerable<Invoice> GetAllForPerson(int personId);
        bool ExistsNumber(string number);
        IQueryable<Invoice> GetQueryable();
    }

}