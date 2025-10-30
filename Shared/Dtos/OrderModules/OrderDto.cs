using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderModules
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set;} 
        public AddressDto Address { get; set; }= default!;
    }
}
