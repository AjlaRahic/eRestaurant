using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System.Drawing;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.Migrations;
using WebApplication4.ModulKorisnik.ViewModels;
using WebApplication4.ModulMeni.ViewModels;

namespace WebApplication4.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public KorisnikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddProfileImage(int id, [FromForm] AddSlikaKorisnikVM x)
        {
            Korisnik korisnik = _dbContext.Korisnik.FirstOrDefault(s => s.Id == id);

            if (korisnik == null)
                return BadRequest("neispravan korisnik ID");
            if (x.slika.Length > 300 * 1000)
                return BadRequest("max velicina fajla je 300 KB");

            string ekstenzija = Path.GetExtension(x.slika.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            x.slika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
            korisnik.Slika = Config.SlikeURL + filename;
            await _dbContext.SaveChangesAsync();

            return Ok(korisnik);

        }

        
        [HttpPost]
        public async Task<IActionResult> Obrisi([FromBody] int y)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;
            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            Korisnik korisnik = _dbContext.Korisnik.Find(y);
            _dbContext.Korisnik.Remove(korisnik);
            await _dbContext.SaveChangesAsync();
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Console.WriteLine("Pokrećem logiranje.");
            if (HttpContext != null && admin != null)
            {
                var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                Console.WriteLine("Pokrećem logiranje.");
                await KretanjePoSistemu.Save(HttpContext, exceptionDetails);
            }
            return Ok();

        }
      
        [HttpGet]

        public List<Korisnik>? GetAllKorisnike()
        {
            var korisnici = _dbContext.Korisnik.ToList();
            return korisnici.Count > 0 ? korisnici : null ;

        }

        [HttpGet]
        public ActionResult<List<Korisnik>> GetKorisnik(string ime_prezime)
        {
            var data = _dbContext.Korisnik
                .Include(s => s.Grad)
                .Where(x => ime_prezime == null || (x.Ime + " " + x.Prezime).StartsWith(ime_prezime) || (x.Prezime + " " + x.Ime).StartsWith(ime_prezime))
                .OrderByDescending(s => s.Id)
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpPost]

        public async Task<IActionResult> UpdateSlika([FromForm] AddSlikaKorisnikVM AddkorisnikSlikaVM, int id)
        {

            if (!HttpContext.GetLoginInfo().isPermsijaKorisnik)
                return BadRequest("nije logiran");
            

            Korisnik korisnik = _dbContext.Korisnik.FirstOrDefault(x => x.Id == id);



            if (AddkorisnikSlikaVM.slika != null && korisnik != null)
            {
                if (AddkorisnikSlikaVM.slika.Length > 250 * 1000)
                    return BadRequest("max velicina fajla je 250 KB");

                string ekstenzija = Path.GetExtension(AddkorisnikSlikaVM.slika.FileName);

                var filename = $"{Guid.NewGuid()}{ekstenzija}";

                AddkorisnikSlikaVM.slika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                korisnik.Slika = Config.SlikeURL + filename;
                await _dbContext.SaveChangesAsync();
            }

            return Ok(korisnik);

        }
        
        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] NoviKorisnikVM x)
        {
            Console.WriteLine($"Podaci primljeni u API: {JsonConvert.SerializeObject(x)}");
           



            Korisnik korisnik;

            if (x.id == 0)
            {
                korisnik = new Korisnik();
                _dbContext.Korisnik.Add(korisnik);
            }
            else
            {

                korisnik = _dbContext.Korisnik.Find(x.id);
                if (korisnik == null)
                    return NotFound("Stavka sa tim ID-om nije pronađena.");

            }
            korisnik.Id = x.id;
            korisnik.Ime = x.ime;
            korisnik.Prezime = x.prezime;
            korisnik.Email = x.email;
            korisnik.Adresa = x.adresaStanovanja;
            korisnik.BrojTelefona = x.brojTelefona;
            korisnik.KorisnickoIme = x.korisnickoIme;
            korisnik.Lozinka = x.lozinka;
            korisnik.GradId = x.gradId;

            await _dbContext.SaveChangesAsync();
           

            return Ok();
        }
       



    }
}

