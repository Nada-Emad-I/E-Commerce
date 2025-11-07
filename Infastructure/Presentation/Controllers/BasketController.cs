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
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager):ControllerBase
    {
        //Get Basket
        [HttpGet] //GET :baseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var basket=await _serviceManager.basketService.GetBasketAsync(Key);
            return Ok(basket);
        }
        //Create Or Update Basket
        [HttpPost] //POST :baseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        //Delete Basket
        [HttpDelete("{Key}")]//Delete :baseUrl/api/Basket/basket01
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result =await _serviceManager.basketService.DeleteBasketAsync(Key);
            return Ok(result);
        }
    }
}
