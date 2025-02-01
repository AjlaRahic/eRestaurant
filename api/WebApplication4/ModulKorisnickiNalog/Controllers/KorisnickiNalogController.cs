using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.ModulKorisnickiNalog.ViewModels;

using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.ModulKorisnik.ViewModels;

using WebApplication4.ModulKorisnickiNalog.ViewModels;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApplication4.ModulKorisnickiNalog.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnickiNalogController : Controller
    {
        private ApplicationDbContext _dbContext;

        public KorisnickiNalogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            GetKorisnikVM korisnikGetVM = new GetKorisnikVM();
            KorisnickiNalog defaultniNalog = new KorisnickiNalog();

            if (HttpContext.GetLoginInfo().isPermsijaKorisnik)
            {
                Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.korisnik;
                if (korisnik == null)
                    return BadRequest("Nepostojeci korisnik");

                defaultniNalog = korisnik;
                korisnikGetVM.BrojTelefona = korisnik.BrojTelefona;
                korisnikGetVM.AdresaStanovanja = korisnik.Adresa;
                korisnikGetVM.GradId = korisnik.GradId;
            }
            /*else if (HttpContext.GetLoginInfo().isPermsijaAdministrator)
            {
              Administrator admin = HttpContext.GetLoginInfo().korisnickiNalog.Administrator;
              if (admin == null)
                return BadRequest("Nepostojeci administrator");
              defaultniNalog = admin;
            }
                  else if (HttpContext.GetLoginInfo().isPermisijaUposlenik)
                  {
                      Uposlenik uposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Uposlenik;
                      if (uposlenik == null)
                          return BadRequest("Nepostojeci zaposlenik");
                      defaultniNalog = uposlenik;
                      korisnikGetVM.Slika = uposlenik.Slika;
                  }
            */
            //korisnikGetVM.Ime = defaultniNalog.im;
            //korisnikGetVM.Prezime = defaultniNalog.;
            //korisnikGetVM.Email = defaultniNalog.Email;
            korisnikGetVM.KorisnickoIme = defaultniNalog.KorisnickoIme;
            korisnikGetVM.Lozinka = defaultniNalog.Lozinka;

            return Ok(korisnikGetVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdministrator([FromBody] AdministratorUpdateVM adminUpdateVM)
        {


            if (!HttpContext.GetLoginInfo().isPermsijaAdministrator)
                return BadRequest("Nije logiran");

            Administrator admin = HttpContext.GetLoginInfo().korisnickiNalog.administrator;
            if (admin == null)
                return BadRequest("Nepostojeci administrator");

            admin.Ime = adminUpdateVM.ime;
            admin.Prezime = adminUpdateVM.prezime;
            admin.Email = adminUpdateVM.email;
            admin.KorisnickoIme = adminUpdateVM.korisnickoIme;
            admin.Lozinka = adminUpdateVM.lozinka;
            admin.DatumKreiranja = DateTime.Now;
            await _dbContext.SaveChangesAsync();
         

            return Ok(admin);

        }
    }
}
