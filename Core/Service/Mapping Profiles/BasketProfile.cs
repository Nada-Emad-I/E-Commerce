using AutoMapper;
using DomainLayer.Models.BasketModules;
using Shared.Dtos.BasketModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping_Profiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket,BasketDto>().ReverseMap();

            CreateMap<BasketItem,BasketItemDto>().ReverseMap();   
        }
    }
}
