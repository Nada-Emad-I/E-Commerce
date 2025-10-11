using AutoMapper;
using DomainLayer.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping_Profiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(Dist => Dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(Dist => Dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(Dist => Dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();

        }
    }
}
