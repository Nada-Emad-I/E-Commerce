using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModules;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping_Profiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {

            if (string.IsNullOrEmpty(source.product.PictureUrl))
                return string.Empty;
            return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.product.PictureUrl}";
        }
    }
}
