
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModules;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extenstions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Data.Contexts;
using Persistence.Identity;
using Persistence.Repositories;
using Service;
using Service.Mapping_Profiles;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            // .Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureService(builder.Configuration);

            builder.Services.AddApplicationService();

            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTServices(builder.Configuration);
            

            #endregion

            var app = builder.Build();

            app.SeedDataAsync();

            #region Configure the HTTP request pipeline
            /// Configure the HTTP request pipeline.
            ///app.Use(async (RequestContext, NextMiddelWire) =>
            ///{
            ///    Console.WriteLine("Request Under Processing");
            ///    await NextMiddelWire.Invoke();
            ///    Console.WriteLine("Waiting Processing");
            ///    Console.WriteLine(RequestContext.Response.Body);
            ///});
            ///
            
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.USeSwaggerMiddleWares();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
