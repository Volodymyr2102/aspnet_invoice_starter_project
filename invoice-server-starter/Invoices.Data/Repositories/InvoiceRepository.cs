using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(InvoicesDbContext context) : base(context) { }

        public IEnumerable<Invoice> GetAllForPerson(int personId)
        {
            return dbSet
                .AsNoTracking()
                .Where(i => i.SellerId == (int?)personId || i.BuyerId ==  (int?)personId)
                .OrderByDescending(i => i.Issued)
                .ToList();
        }
        public bool ExistsNumber(string number)
        {
            return false;
        }
        public IQueryable<Invoice> GetQueryable()
        {
            return context.Invoices.AsQueryable();
        }
    }
}