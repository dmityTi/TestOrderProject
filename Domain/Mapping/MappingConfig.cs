using System;
using AutoMapper;
using AutoMapper.Configuration;
using Domain.DTOs.OrderAggregate;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Mapping
{
    public class MappingConfig
    {
        public static void RegisterMapping(MapperConfigurationExpression mapperConfigurationExpression)
        {
            InitOrderMapping(ref mapperConfigurationExpression);
            
            Mapper.Initialize(mapperConfigurationExpression);
        }

        private static void InitOrderMapping(ref MapperConfigurationExpression config)
        {
            config.CreateMap<Order, OrderDTO>()
                .ForMember(d => d.Status, 
                    opt => opt.MapFrom(s => s.Status.ToString()));

            config.CreateMap<OrderDTO, Order>()
                .ForMember(d => d.Status, 
                    opt => opt.MapFrom(x => Enum.Parse(typeof(OrderStatus), x.Status, true)))
                .ForMember(d => d.BillingAddressId, opt => opt.Ignore());

            config.CreateMap<ArticleItem, Article>()
                .ForMember(d => d.OrderId, opt => opt.Ignore());
            
            config.CreateMap<PaymentItem, Payment>()
                .ForMember(d => d.OrderId, opt => opt.Ignore());

            config.CreateMap<BillingAddressItem, BillingAddress>()
                .ForMember(d => d.OrderId, opt => opt.Ignore());
            
            config.CreateMap<Article, ArticleItem>();
            config.CreateMap<BillingAddress, BillingAddressItem>();
            config.CreateMap<Payment, PaymentItem>();
        }
        
    }
}