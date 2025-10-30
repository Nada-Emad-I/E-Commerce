using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager _serviceManager):ApiBaseController
    {
        //Create Order
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var order=await _serviceManager.orderService.CreateOrderAsync(orderDto,GetEmailByToken());
            return Ok(order);
        }
        //GetDeliveryMethods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods=await _serviceManager.orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

        //GetAllOrderByEmail
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>>GetAllOrders()
        {
            var orders = await _serviceManager.orderService.GetAllOrdersAsync(GetEmailByToken());
            return Ok(orders);
        }

        //GetAllOrderById
        //[Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>>GetOrderById(Guid id)
        {
            var order = await _serviceManager.orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
