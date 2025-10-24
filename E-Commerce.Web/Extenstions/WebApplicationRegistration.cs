using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extenstions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();

            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await seed.DataSeedAsync();
            await seed.IdentityDataSeedAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app) 
        {
            app.UseMiddleware<CustomExpectionHandlerMiddleWare>();
            return app;
        }
        public static IApplicationBuilder USeSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
