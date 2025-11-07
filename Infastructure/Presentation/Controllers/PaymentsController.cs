using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.BasketModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager serviceManager):ApiBaseController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>>CreateOrUpdate(string basketId)
        {
           var basket=await serviceManager.paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(basket);
        }
        [HttpPost("WebHook")]
        public async Task<ActionResult> WebHook()
        {
            var json =await new StreamReader(HttpContext.Response.Body).ReadToEndAsync();
            await serviceManager.paymentService.UpdateOrderPaymentStatusAsync(json, Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }
    }
}
