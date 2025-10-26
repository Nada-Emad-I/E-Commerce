using AutoMapper;
using DomainLayer.Models.IdentityModules;
using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping_Profiles
{
    internal class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
