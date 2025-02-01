using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.Functions;
using System.Net;
using System.Text.Json;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Service;
using XAct.Library.Settings;

namespace WebApplication4.Helper.ErrorHandler
{
    public class ErrorHandlerMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IEmailService emailService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                //emailService.sendMail(error.Message);

                if (error is ErrorResponse)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    switch (error)
                    {
                        case KeyNotFoundException:
                            // not found error
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        default:
                            // unhandled error
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                }
                var message = error.Message;
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}

