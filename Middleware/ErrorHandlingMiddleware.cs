﻿using WarehouseAPI.Exceptions;

namespace WarehouseAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next.Invoke(context);
            }
            catch (BadRequestException badRequest)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(badRequest.Message);
            }
        }
    }
}

