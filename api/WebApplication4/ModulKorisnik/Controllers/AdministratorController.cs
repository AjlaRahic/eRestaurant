using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.ModulKorisnik.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.ModulKorisnik.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]

    public class AdministratoriController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;

        public AdministratoriController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public ActionResult<List<Administrator>> GetAdministrator(string ime_prezime)
        {
            var data = _dbcontext.Administrator
                .Include(s => s.Grad)
                .Where(x => ime_prezime == null || (x.Ime + " " + x.Prezime).StartsWith(ime_prezime) || (x.Prezime + " " + x.Ime).StartsWith(ime_prezime))
                .OrderByDescending(s => s.Id)
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]

        public List<Administrator>? GetAllAdministratore()
        {
            var administratori = _dbcontext.Administrator.ToList();
            return administratori.Count > 0 ? administratori : null;

        }
        [HttpPost]
        public async Task<IActionResult> Obrisi([FromBody] int y)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;
            var admin = _dbcontext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            Administrator administrator = _dbcontext.Administrator.Find(y);
            _dbcontext.Administrator.Remove(administrator);
            await _dbcontext.SaveChangesAsync();
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

        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] NoviAdminVM x)
        {
            var admin = new Administrator();

            if (x.id == 0)
            {

                _dbcontext.Administrator.Add(admin);
            }
            else
            {
                admin = _dbcontext.Administrator.Find(x.id);

            }
            admin.Id = x.id;
            admin.Ime = x.ime;
            admin.Prezime = x.prezime;
            admin.Email = x.email;
            admin.Adresa = x.adresaStanovanja;
            admin.BrojTelefona = x.brojTelefona;
            admin.KorisnickoIme = x.korisnickoIme;
            admin.Lozinka = x.lozinka;
            admin.GradId = x.gradId;

            await _dbcontext.SaveChangesAsync();
            return Ok();
        }



       
        [HttpGet]

        public async Task<List<Administrator>> GetAll()
        {
            return await _dbcontext.Administrator.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> DodajSliku(int id, [FromForm] AddSlikaKorisnikVM x)
        {
            Administrator administrator = _dbcontext.Administrator.FirstOrDefault(s => s.Id == id);

            if (administrator == null)
                return BadRequest("neispravan korisnik ID");
            if (x.slika.Length > 300 * 1000)
                return BadRequest("max velicina fajla je 300 KB");

            string ekstenzija = Path.GetExtension(x.slika.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            x.slika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
            administrator.Slika = Config.SlikeURL + filename;
            await _dbcontext.SaveChangesAsync();

            return Ok(administrator);

        }


    }

}


