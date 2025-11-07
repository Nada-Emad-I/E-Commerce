using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModules;
using Shared.Dtos.BasketModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketModel=_mapper.Map<BasketDto,Basket>(basket);
            var CreatedOrUpdatedBasket=await _basketRepository.CreateOrUpdateBasketAsync(basketModel);
            if (CreatedOrUpdatedBasket != null)
            {
                return await GetBasketAsync(basket.Id);
            }
            throw new Exception("Can't Create or update Basket right Now,Please Try Again Later");
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _basketRepository.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket=await _basketRepository.GetBasketAsync(Key);
            if (basket is not null)
            {
                return _mapper.Map<Basket,BasketDto>(basket);
            }
            throw new BasketNotFoundException(Key);
        }
    }
}
