using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Entities
{
    public class Invoice 
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceId { get; set; }

    [Required]
    public int InvoiceNumber { get; set; }

    [Required]
    public DateOnly Issued { get; set; }

    [Required]
    public DateOnly DueDate { get; set; }

    [Required]
    public string Product { get; set; } = "";

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Vat { get; set; }

    [Required]
    public string Note { get; set; } = "";

    public int? SellerId { get; set; }
    public int? BuyerId { get; set; }

    [ForeignKey(nameof(SellerId))]
    public virtual Person? Seller { get; set; }

    [ForeignKey(nameof(BuyerId))]
    public virtual Person? Buyer { get; set; }
}
}