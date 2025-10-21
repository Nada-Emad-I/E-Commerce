﻿using AutoMapper;
using DomainLayer.Models.ProductModules;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.ProductModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping_Profiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            return $"{_configuration.GetSection("Urls")["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
