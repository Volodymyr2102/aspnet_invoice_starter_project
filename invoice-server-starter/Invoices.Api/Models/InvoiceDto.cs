using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class InvoiceDto
    {
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }  
        public int InvoiceNumber { get; set; }

        public DateOnly Issued { get; set; }
        public DateOnly DueDate { get; set; }
        public string Product { get; set; } = "";
        public decimal Price { get; set; }
        public int Vat { get; set; }
        public string Note { get; set; } = "";

       
        public MinimalPersonDto? Seller { get; set; }
        public MinimalPersonDto? Buyer { get; set; }
    }

    public class MinimalPersonDto
    {
        [JsonPropertyName("_id")]
        public int PersonId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}

