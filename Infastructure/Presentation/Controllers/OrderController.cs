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
    public class OrderController(IServiceManager _serviceManager):ApiBaseController
    {
        //Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var order=await _serviceManager.orderService.CreateOrderAsync(orderDto,GetEmailByToken());
            return Ok(order);
        }

    }
}
