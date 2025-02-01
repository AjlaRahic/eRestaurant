using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.Migrations;

namespace WebApplication4.Helper.AutentifikacijaAutorizacija
{
    public class KretanjePoSistemu
    {
        public static async Task<int> Save(HttpContext httpContext, IExceptionHandlerPathFeature exceptionMessage = null)
        {
            Console.WriteLine("metoda save se pozove");

            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();
            KorisnickiNalog korisnickinalog = httpContext.GetLoginInfo()?.korisnickiNalog;
            var admin = db.Administrator
                          .FirstOrDefault(a => a.Id == korisnickinalog.Id);
            var request = httpContext.Request;

            string detalji = "";

            if (request.ContentLength.HasValue && request.ContentLength.Value > 0)
            {
                using (var reader = new StreamReader(request.Body))
                {
                    string body = await reader.ReadToEndAsync();
                    Console.WriteLine("JSON Body: " + body); 
                    detalji = body;  
                }
            }
            else
            {
                Console.WriteLine("Tijelo zahteva je prazno.");
            }

            var x = new LogKretanjeSistem
            {
                AdministratorId = admin?.Id??0,
                Vrijeme = DateTime.Now,
                QueryPath = request.GetEncodedPathAndQuery(),
                PostData = detalji,
                IpAdresa = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.ExceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }
            if (exceptionMessage == null)
            {
                Console.WriteLine("Nema exceptiona za logiranje.");
            }

           db.Add(x);
           await db.SaveChangesAsync();  
           return 0;
        }



    }
}
