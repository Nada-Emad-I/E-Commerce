using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModules;
using DomainLayer.Models.OrderModules;
using DomainLayer.Models.ProductModules;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Specifications;
using ServiceAbstraction;
using Shared.Dtos.BasketModules;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService(IConfiguration _configuration
        ,IBasketRepository _basketRepository
        ,IUnitOfWork _unitOfWork
        ,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["SecretKey"];

            var basket=await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            var productRepo= _unitOfWork.GetRepository<DomainLayer.Models.ProductModules.Product, int>();
            foreach (var item in basket.Items)
            {
                var product =await productRepo.GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }

            if (basket.DeliveryMethodId is null) throw new ArgumentNullException();
            var deliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value);

            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippigPrice = deliveryMethod.Price;

            var amount =(long) ((basket.Items.Sum(I => I.Price * I.Quantity)+basket.ShippigPrice)*100);
            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))//create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency="AED",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent=await service.CreateAsync(options);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else //Update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);
            }

            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return _mapper.Map<Basket, BasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader)
        {
            var endPointSecret = _configuration.GetSection("Stripe")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeader, endPointSecret);

            var PaymentIntent=stripeEvent.Data.Object as PaymentIntent;
            switch(stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFailedAsync(PaymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentRecievedAsync(PaymentIntent.Id);
                    break;
                default:
                    Console.WriteLine($"Unhandled Stripe Event Types {stripeEvent.Type}");
                    break;
            }
        }
        private async Task UpdatePaymentRecievedAsync(string paymentIntentId)
        {
            var order =await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));
            order.OrderStatus = OrderStatus.PaymentRecieved;
            _unitOfWork.GetRepository<Order, Guid>()
                .Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order =await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));
            order.OrderStatus = OrderStatus.PaymentFailed;
            _unitOfWork.GetRepository<Order, Guid>()
                .Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
