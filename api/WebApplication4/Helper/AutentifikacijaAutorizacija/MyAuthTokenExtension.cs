using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using WebApplication4.Data;
using WebApplication4.EntityModels;


namespace WebApplication4.Helper.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }
            [JsonIgnore]
            public KorisnickiNalog korisnickiNalog => autentifikacijaToken?.KorisnickiNalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            public bool isLogiran => korisnickiNalog != null;
            public bool isPermsijaKorisnik => isLogiran && (korisnickiNalog.isKorisnik);
            public bool isPermsijaAdministrator => isLogiran && (korisnickiNalog.isAdministrator);
            public bool isPermisijaUposlenik => isLogiran && (korisnickiNalog.isUposlenik);
            public bool isPermisijaGost => !isLogiran;

        }
        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            AutentifikacijaToken token = httpContext.GetAuthToken();
            return new LoginInformacije(token);
        }
        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            // Dohvati token iz zaglavlja
            string token = httpContext.GetMyAuthToken();

            // Ako token nije pronađen, vrati null
            if (string.IsNullOrEmpty(token))
                return null;

            // Dohvati ApplicationDbContext
            var db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            // Pokušaj pronaći token u bazi
            var korisnickiNalog = db.AutentifikacijaToken
                .Include(s => s.KorisnickiNalog)
                .SingleOrDefault(x => x.Vrijednost == token);

            // Provjeri da li je token pronađen
            if (korisnickiNalog == null)
            {
                Console.WriteLine($"Token nije pronađen ili je istekao: {token}");
                return null;
            }

            // Provjeri da li je korisnik povezan sa tokenom
            if (korisnickiNalog.KorisnickiNalog == null)
            {
                Console.WriteLine($"Token je validan, ali korisnik nije povezan: {token}");
                return null;
            }

            return korisnickiNalog;
        }
        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
