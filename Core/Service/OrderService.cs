﻿using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModules;
using DomainLayer.Models.ProductModules;
using ServiceAbstraction;
using Shared.Dtos.IdentityModules;
using Shared.Dtos.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository
        ,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            //Map AddressDto to OrderAddress
            var address =_mapper.Map<AddressDto,OrderAddress>(orderDto.Address);

            //GetBasket
            var basket =await _basketRepository.GetBasketAsync(orderDto.BasketId);
            if (basket is null)
                throw new BasketNotFoundException(orderDto.BasketId);

            //CreateOrderItem List
            List<OrderItem>orderItems = new List<OrderItem>();

            var productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product =await productRepo.GetByIdAsync(item.Id);
                if (product is null)
                    throw new ProductNotFoundException(item.Id);

                var orderItem = new OrderItem()
                {
                    product = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }

            //Get Delivery Method
            var deliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            //Calculate order
            var subTotal = orderItems.Sum(OI => OI.Quantity * OI.Price);
            //var order=new Order()
            var order =new Order(email,address,deliveryMethod,orderItems,subTotal);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }
    }
}
