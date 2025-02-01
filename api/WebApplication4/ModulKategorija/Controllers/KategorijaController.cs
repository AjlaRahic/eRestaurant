using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.ModulKategorija.ViewModels;

namespace WebApplication4.ModulKategorija.Controllers
{
    [ApiController]
    [Route("[Controller]/[action]")]
    public class KategorijaController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public KategorijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] SnimiKategorijaVM x)
        {
            Kategorija? objekat;

            if (x.Id == 0)
            {
                objekat = new Kategorija();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Kategorija.Find(x.Id);
            }
            objekat.Naziv = x.Naziv;
            await _dbContext.SaveChangesAsync();
            return Ok(objekat);
        }
        [HttpGet]
        public async Task<ActionResult> GetALL()
        {
            var data = await _dbContext.Kategorija
                .OrderBy(x => x.Naziv)
                .Select(x => new GetALLKategorijaVM
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                })
                .Take(100)
                .ToListAsync(); 

            return Ok(data);
        }

    }
}
