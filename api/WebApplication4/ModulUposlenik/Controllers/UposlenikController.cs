using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.ModulUposlenik.ViewModels;

namespace WebApplication4.ModulUposlenik.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UposlenikController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public UposlenikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> DodajUposlenik([FromBody] NoviUposlenik x)
        {
            var uposlenik = new Uposlenik();

            if (x.id == 0)
            {

                _dbContext.Uposlenici.Add(uposlenik);
            }
            else
            {
                uposlenik = _dbContext.Uposlenici.Find(x.id);

            }
            uposlenik.Id = x.id;
            uposlenik.Slika = x.Slika;
            uposlenik.KorisnickoIme = x.korisnickoIme;
            uposlenik.Lozinka = x.lozinka;

            _dbContext.SaveChanges();
            return Ok();
        } 
        [HttpGet]
        public async Task<List<Uposlenik>> GetAll()
        {
            return await _dbContext.Uposlenici.ToListAsync();
        }
       
       
    } 
        
       
       

    
}
