using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.ModulKorisnik.ViewModels;
using WebApplication4.ModulMeni.ViewModels;
using WebApplication4.ModulSkladiste.ViewModels;

namespace WebApplication4.ModulSkladiste.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UlazController : ControllerBase
    {
        private ApplicationDbContext _dbContext;


        public UlazController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpGet]
        public async Task<List<UlazUSkladiste>> GetAll()
        {
            return await _dbContext.UlazUSkladiste.ToListAsync();
        }
        [HttpPost]

        public async Task<IActionResult> DodajStavku([FromBody] UlazUSkladisteVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            // Provjera da li je korisnik administrator u bazi podataka
            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");


            UlazUSkladiste novi;
            if (x.Id == 0)
            {
                novi = new UlazUSkladiste();
                _dbContext.UlazUSkladiste.Add(novi);
            }
            else
            {
                novi = _dbContext.UlazUSkladiste.Find(x.Id);
            }

            novi.DatumPrijema = x.DatumPrijema;
            novi.Naziv=x.Naziv;
            novi.ImeDobavljaca = x.ImeDobavljaca;
            novi.KolicinaUlaza = x.KolicinaUlaza;
            novi.Cijena = x.Cijena;

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
        [HttpPost]
        public async Task<IActionResult> Obrisi([FromBody] int y)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            // Provjera da li je korisnik administrator u bazi podataka
            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            UlazUSkladiste ulaz = _dbContext.UlazUSkladiste.Find(y);
            _dbContext.UlazUSkladiste.Remove(ulaz);
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

        public async Task<IActionResult> GetId([FromBody] int y)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            // Provjera da li je korisnik administrator u bazi podataka
            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            var ulaz = _dbContext.UlazUSkladiste.Where(x => x.Id == y).ToList();
            return Ok(ulaz);

        }
        [HttpGet]
        public async Task<IActionResult> GetUlaziPoPeriodu(DateTime startDate, DateTime endDate)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;
            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);
            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            var result = _dbContext.UlazUSkladiste
                .Where(u => u.DatumPrijema >= startDate && u.DatumPrijema <= endDate)
                .GroupBy(u => u.DatumPrijema.Date)
                .Select(g => new
                {
                    Datum = g.Key,
                    TotalKolicina = g.Sum(u => u.KolicinaUlaza),
                    TotalCijena = g.Sum(u => u.Cijena * u.KolicinaUlaza)
                })
                .OrderBy(g => g.Datum)
                .ToList();
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Console.WriteLine("Pokrećem logiranje.");
            if (HttpContext != null && admin != null)
            {
                var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                Console.WriteLine("Pokrećem logiranje.");
                await KretanjePoSistemu.Save(HttpContext, exceptionDetails);
            }

            return Ok(result);
        }
    }



}
