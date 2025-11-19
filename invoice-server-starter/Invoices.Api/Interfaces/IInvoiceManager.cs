using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    public interface IInvoiceManager
    {
        IEnumerable<InvoiceDto> GetAll();
        InvoiceDto? GetById(int id);
        InvoiceDto Create(InvoiceDto dto);
        InvoiceDto? Update(int id, InvoiceDto dto);
        bool Delete(int id);
    }
}