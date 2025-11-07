using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class ServiceManagerWithFactoryDelegate(Func<IProductService> ProductFactory
        ,Func<IBasketService> BasketFactory,Func<IAuthenticationService> AuthorizationFactory
        ,Func<IOrderService>OrderFactory) //: IServiceManager
    {
        public IProductService productService => ProductFactory.Invoke();

        public IBasketService basketService => BasketFactory.Invoke();

        public IAuthenticationService authenticationService => AuthorizationFactory.Invoke();

        public IOrderService orderService => OrderFactory.Invoke();
    }
}
