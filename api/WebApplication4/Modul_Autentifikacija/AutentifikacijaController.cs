using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper;
using WebApplication4.Modul_Autentifikacija.ViewModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using static WebApplication4.Helper.AutentifikacijaAutorizacija.MyAuthTokenExtension;

namespace WebApplication4.ModulAutentifikacija.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]

    public class AutentifikacijaController : ControllerBase
    {
        private ApplicationDbContext _dbContext;


        public AutentifikacijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            KorisnickiNalog logiraniKorisnik = _dbContext.KorisnickiNalog
                .FirstOrDefault(k => k.KorisnickoIme != null && k.KorisnickoIme == loginVM.korisnickoIme && k.Lozinka == loginVM.lozinka);

            if (logiraniKorisnik == null)
            {

                return Ok(new LoginInformacije(null));
            }

            string randomString = TokenGenerator.Generate(10);


            var noviToken = new AutentifikacijaToken()
            {
                IpAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Vrijednost = randomString,
                KorisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now
            };

            await _dbContext.AutentifikacijaToken.AddAsync(noviToken);
            await _dbContext.SaveChangesAsync();

            return Ok(new LoginInformacije(noviToken));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();
            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
           await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();
            return autentifikacijaToken;
        }
        [HttpGet]
        public async Task<ActionResult<KorisnickiNalog>> GetUser(int id)
        {
            var user = await _dbContext.KorisnickiNalog.FindAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


    }
}
