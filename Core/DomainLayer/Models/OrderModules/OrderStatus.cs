using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModules
{
    public enum OrderStatus
    {
        Pending=0,
        PaymentRecieved,
        PaymentFailed
    }
}
