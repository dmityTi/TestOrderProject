using AutoMapper.Configuration;
using Domain.DTOs.OrderAggregate;
using TestOrderProjectWebAPI.Models.RequestOrderAggregate;
using BillingAddress = TestOrderProjectWebAPI.Models.RequestOrderAggregate.BillingAddress;
using Payment = TestOrderProjectWebAPI.Models.RequestOrderAggregate.Payment;

namespace TestOrderProjectWebAPI.Mapping
{
    public class MappingConfig
    {
        public static void RegisterMapping()
        {
            var mapperConfig = new MapperConfigurationExpression();
            Init(ref mapperConfig);
            Domain.Mapping.MappingConfig.RegisterMapping(mapperConfig);
        }

        private static void Init(ref MapperConfigurationExpression config)
        {
            config.CreateMap<OrderRequestModel, OrderDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Oxid))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.OrderDate))
                .ForMember(d => d.Status, opt => opt.Ignore())
                .ForMember(d => d.InvoiceNumber, opt => opt.Ignore())

                .ForMember(d => d.Articles, opt => opt.MapFrom(s => s.Articles.Articles))
                .ForMember(d => d.Payments, opt => opt.MapFrom(s => s.Payments.Payments));

            config.CreateMap<BillingAddress, BillingAddressItem>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country.Geo))
                .ForMember(d => d.HomeNumber, opt => opt.MapFrom(s => s.StreetNr));

            config.CreateMap<Payment, PaymentItem>();
            config.CreateMap<OrderArticle, ArticleItem>()
                .ForMember(d => d.Nomenclature, opt => opt.MapFrom(s => s.ArtNum));
        }
    }
}