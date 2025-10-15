using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]//baseUrl/api/controller
    public class ProductsController(IServiceManager _serviceManager):ControllerBase 
    {
        //Get All Products
        [HttpGet]
        //baseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>>GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products=await _serviceManager.productService.GetAllProductsAsync(queryParams);
            return Ok(products);

        }
        //Get  Product by id
        [HttpGet("{id}")]
        //baseUrl/api/Product/10
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product=await _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        //Get All Brands
        [HttpGet("brands")]
        //baseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands=await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(brands);
        }
        //Get All types
        [HttpGet("types")]
        //baseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.productService.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
