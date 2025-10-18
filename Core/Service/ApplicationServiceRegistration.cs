using Microsoft.Extensions.DependencyInjection;
using Service.Mapping_Profiles;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(Service.AssemblyReference).Assembly); 

            return Services;
        }
    }
}
