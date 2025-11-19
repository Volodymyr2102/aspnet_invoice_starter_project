using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Api.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        public InvoiceManager(IInvoiceRepository invoiceRepository, IPersonRepository personRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public IEnumerable<InvoiceDto> GetAll()
        {
            var invoices = invoiceRepository.GetQueryable()
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .ToList();

            return mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public InvoiceDto? GetById(int id)
        {
            var invoice = invoiceRepository.GetQueryable()
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .FirstOrDefault(i => i.InvoiceId == id);

            return invoice == null ? null : mapper.Map<InvoiceDto>(invoice);
        }

        public InvoiceDto Create(InvoiceDto dto)
        {
            var entity = mapper.Map<Invoice>(dto);

            if (dto.Seller != null)
                entity.Seller = personRepository.FindById(dto.Seller.PersonId);

            if (dto.Buyer != null)
                entity.Buyer = personRepository.FindById(dto.Buyer.PersonId);

            var added = invoiceRepository.Add(entity);
            invoiceRepository.SaveChanges();

            return mapper.Map<InvoiceDto>(added);
        }

        public InvoiceDto? Update(int id, InvoiceDto dto)
        {
            var existing = invoiceRepository.FindById(id);
            if (existing == null)
                return null;

            // Сюди приїде DTO без ключа, але ми його ігноруємо в мапінгу
            mapper.Map(dto, existing);

            if (dto.Seller != null)
                existing.Seller = personRepository.FindById(dto.Seller.PersonId);

            if (dto.Buyer != null)
                existing.Buyer = personRepository.FindById(dto.Buyer.PersonId);

            invoiceRepository.Update(existing);
            invoiceRepository.SaveChanges();

            return mapper.Map<InvoiceDto>(existing);
        }

        public bool Delete(int id)
        {
            var invoice = invoiceRepository.FindById(id);
            if (invoice == null)
                return false;

            invoiceRepository.Delete(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }
    }
}