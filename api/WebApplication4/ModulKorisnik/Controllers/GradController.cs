using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;

namespace WebApplication4.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GradController : ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public GradController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]


        public async Task<List<CmbStavke>> GetAll()
        {
            return await _dbContext.Grad
                .OrderBy(s => s.Naziv)
                .Select(s => new CmbStavke
                {
                    id = s.Id,
                    opis = s.Naziv
                })
                .Take(100)
                .ToListAsync(); 
        }


        [HttpPost]

        public async Task<IActionResult> Dodaj(string naziv)
        {
            Grad novigrad = new Grad() { Naziv = naziv };
            _dbContext.Grad.Add(novigrad);
            await _dbContext.SaveChangesAsync();
            return Ok(novigrad.Id);
        }
        [HttpPost]

        public async Task<IActionResult>Obrisi(int id)
        {
            Grad grad = _dbContext.Grad.Find(id);

            if (grad == null)//|| id == 1
                return BadRequest("ne postoji taj ID");

            _dbContext.Remove(grad);

            await _dbContext.SaveChangesAsync();
            return Ok(grad);
        }
    }
}
