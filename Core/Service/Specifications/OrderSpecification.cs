using DomainLayer.Models.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class OrderSpecification : BaseSpecification<Order, Guid>
    {
        //Get All Orders By Email
        public OrderSpecification(string email) : base(O=>O.UserEmail==email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O=>O.OrderDate);
        }
        //Get All Orders By Id
        public OrderSpecification(Guid Id) : base(O=>O.Id==Id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
