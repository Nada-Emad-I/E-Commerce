using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModules
{
    public class OrderItem:BaseEntity<int>
    {
        public ProductItemOrder product { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
