using System.Runtime.InteropServices;
using WarehouseAPI.Exceptions;

namespace WarehouseAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch(NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }
            catch(AlreadyExistExceptions alreadyExist)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(alreadyExist.Message);
            }
        }

    }
}


