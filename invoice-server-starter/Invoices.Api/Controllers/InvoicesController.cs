using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceManager invoiceManager;

        public InvoicesController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }

        // GET: api/invoices
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int? year = null,
            [FromQuery] string? product = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            var invoices = invoiceManager.GetAll();

            //  rok
            if (year.HasValue)
            {
                invoices = invoices
                    .Where(i => i.Issued.Year == year.Value)
                    .ToList();
            }

            //  produkt
            if (!string.IsNullOrWhiteSpace(product))
            {
                invoices = invoices
                    .Where(i => i.Product != null &&
                                i.Product.Contains(product, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            //  Filtr min
            if (minPrice.HasValue)
            {
                invoices = invoices
                    .Where(i => i.Price >= minPrice.Value)
                    .ToList();
            }

            //  Filtr max 
            if (maxPrice.HasValue)
            {
                invoices = invoices
                    .Where(i => i.Price <= maxPrice.Value)
                    .ToList();
            }

            return Ok(invoices);
        }

        // GET: api/invoices/{id}
        [HttpGet("{id}")]
        public ActionResult<InvoiceDto> GetById(int id)
        {
            var invoice = invoiceManager.GetById(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        // POST: api/invoices
        [HttpPost]
        public ActionResult<InvoiceDto> Create([FromBody] InvoiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = invoiceManager.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.InvoiceId }, created);
        }

        // PUT: api/invoices/{id}
        [HttpPut("{id}")]
        public ActionResult<InvoiceDto> Update(int id, [FromBody] InvoiceDto dto)
        {
            var updated = invoiceManager.Update(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/invoices/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = invoiceManager.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("stats")]
        public IActionResult GetStatistics()
        {
            var invoices = invoiceManager.GetAll();

            if (invoices == null || !invoices.Any())
                return Ok(new { Message = "Žádné faktury nebyly nalezeny." });

            var totalCount = invoices.Count();
            var totalPrice = invoices.Sum(i => i.Price);
            var averageVat = invoices.Average(i => i.Vat);
            var totalWithVat = invoices.Sum(i => i.Price * (1 + (decimal)i.Vat / 100));

            //  Výpočet podle dodavatelů
            var companyTurnovers = invoices
                .Where(i => i.Seller != null && i.Seller.PersonId != 0)
                .GroupBy(i => i.Seller.PersonId)
                .Select(g => new
                {
                    sellerId = g.Key,
                    invoicesCount = g.Count(),
                    totalTurnover = g.Sum(i => i.Price * (1 + (decimal)i.Vat / 100))
                })
                .ToList();

            return Ok(new
            {
                totalCount,
                totalPrice,
                averageVat,
                totalWithVat,
                companyTurnovers 
            });
        }
    }
}