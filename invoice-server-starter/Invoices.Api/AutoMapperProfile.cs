using AutoMapper;
using Invoices.Api.Models;
using Invoices.Data.Entities;

namespace Invoices.Api
{
    /// <summary>
    /// Profil pro AutoMapper – definuje převody mezi entitami a DTO třídami.
    /// Odděluje databázovou vrstvu (Entity) od API vrstvy (DTO).
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Osoby
            CreateMap<Person, PersonDto>().ReverseMap();

            CreateMap<Person, MinimalPersonDto>()
                .ForMember(d => d.PersonId, o => o.MapFrom(s => s.PersonId));
            // ReverseMap тут не обов'язковий

            // Faktury: ENTITY -> DTO
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(d => d.Seller, o => o.MapFrom(s => s.Seller))
                .ForMember(d => d.Buyer, o => o.MapFrom(s => s.Buyer));

            // Faktury: DTO -> ENTITY (для Create/Update)
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(d => d.InvoiceId, o => o.Ignore())
                .ForMember(d => d.Seller, o => o.Ignore())
                .ForMember(d => d.Buyer, o => o.Ignore());
        }
    }
}