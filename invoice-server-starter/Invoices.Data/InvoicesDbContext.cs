using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data
{
    /// <summary>
    /// Главный DbContext для приложения Invoices.
    /// </summary>
    public class InvoicesDbContext : DbContext
    {
        public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options)
            : base(options) { }

        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Invoice>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);
            modelBuilder
                .Entity<Invoice>()
               .HasOne(i => i.Buyer)
               .WithMany(p => p.Purchases)
               .HasForeignKey(i => i.BuyerId);

            modelBuilder
                .Entity<Invoice>()
                .HasOne(i => i.Seller)
               .WithMany(p => p.Sales)
               .HasForeignKey(i => i.SellerId);

            // Person
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(p => p.Country).HasConversion<string>();
                builder.HasIndex(p => p.IdentificationNumber);
                builder.HasIndex(p => p.Hidden);
            });
        }
    }
}