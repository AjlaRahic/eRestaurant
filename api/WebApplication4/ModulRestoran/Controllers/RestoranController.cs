using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.ModulKorisnik.ViewModels;
using WebApplication4.ModulMeni.ViewModels;
using WebApplication4.ModulRestoran.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.ModulRestoran.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RestoranController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public RestoranController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
       
        public async Task<List<Restoran>> GetAll()
        {
            return await _dbContext.Restorani.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> DodajRestoran([FromBody] RestoranVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermsijaKorisnik)
                return BadRequest("nije logiran");
            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Restoran novi;
            if (x.Id == 0)
            {
                novi = new Restoran();
                _dbContext.Restorani.Add(novi);
            }
            else
            {
                novi = _dbContext.Restorani.Find(x.Id);
            }

            novi.Id = x.Id;
            novi.Adresa = x.Adresa;
            novi.RadnoVrijemeVikendom = x.RadnoVrijemeVikendom;
            novi.RadnoVrijemeRadnimDanima = x.RadnoVrijemeRadnimDanima;
            novi.GradId = x.GradId;
            novi.Telefon = x.Telefon;


            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]


        public async Task<IActionResult> ObrisiRestoran([FromBody] int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");
            Administrator admin = HttpContext.GetLoginInfo().korisnickiNalog.administrator;

            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            var restoran = _dbContext.Restorani.Find(id);

            _dbContext.Restorani.Remove(restoran);


            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}