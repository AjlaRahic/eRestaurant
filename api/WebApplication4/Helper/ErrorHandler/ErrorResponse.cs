using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace WebApplication4.Helper.ErrorHandler
{
    public class ErrorResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }

       public ErrorResponse() : base() { }

        public ErrorResponse(string Message)  {
            message = Message;
        }//base(message) { }

        public ErrorResponse(string Message, params object[] args)
        {
            message = String.Format(CultureInfo.CurrentCulture, message, args);
            
        }
    }
}
