using Shared.Dtos.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        //create Order
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto order,string email);
        //Get Delivery Method
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        //Get All Orders
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        //Get Order By Id 
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);
    }
}
