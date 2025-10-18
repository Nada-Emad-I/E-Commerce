﻿using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExpectionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExpectionHandlerMiddleWare> _logger;

        public CustomExpectionHandlerMiddleWare(RequestDelegate  next,ILogger<CustomExpectionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //Request
                await _next.Invoke(httpContext);
                //Response
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Worng");
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Set Status Code For Response

            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            //Set Content Type For Response
            //httpContext.Response.ContentType="application/json";

            //Response Object
            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            //Transfer Object To Json
            //var responseJson=JsonSerializer.Serialize(response);

            //await httpContext.Response.WriteAsync(responseJson);

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is not found"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
