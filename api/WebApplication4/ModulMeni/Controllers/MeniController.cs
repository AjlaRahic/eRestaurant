using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.ModulKorisnik.ViewModels;
using WebApplication4.ModulMeni.ViewModels;



namespace WebApplication4.ModulMeni.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MeniController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public MeniController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<List<MeniStavka>> GetAll()
        {
            return await _dbContext.MeniStavka.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> AddStavka([FromBody] AddMeniVM meniVM)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            // Provjera da li je korisnik administrator u bazi podataka
            var admin = _dbContext.Administrator
                                  .FirstOrDefault(a => a.Id == korisnickiNalog.Id);

            if (admin == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            if (admin != null)
            {
                Console.WriteLine("admin je" + admin.Id);
            }

            

            MeniStavka novi;

            if (meniVM.Id == 0)
            {
                novi = new MeniStavka();
                _dbContext.MeniStavka.Add(novi);
            }
            else
            {
                novi = _dbContext.MeniStavka.Find(meniVM.Id);
                if (novi == null)
                    return NotFound("Stavka sa tim ID-om nije pronađena.");
            }

            // Ažuriranje podataka u bazi
            novi.Naziv = meniVM.Naziv;
            novi.Opis = meniVM.Opis;
            novi.Cijena = meniVM.Cijena;
            novi.Izdvojeno = meniVM.Izdvojeno;
            novi.SnizenaCijena = meniVM.SnizenaCijena;
            novi.Ocjena = meniVM.Ocjena;
            novi.KategorijaId = meniVM.KategorijaId;

            // Spremanje promjena u bazu
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
        public async Task<IActionResult>AddMeniSlika(int id, [FromForm] AddSlikaMeniVM x)
        {
            MeniStavka stavka = _dbContext.MeniStavka.FirstOrDefault(s => s.Id == id);

            if (stavka == null)
                return BadRequest("neispravan stavka ID");
            if (x.slika.Length > 300 * 1000)
                return BadRequest("max velicina fajla je 300 KB");

            string ekstenzija = Path.GetExtension(x.slika.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            x.slika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
            stavka.Slika = Config.SlikeURL + filename;
            await _dbContext.SaveChangesAsync();

            return Ok(stavka);

        }


        [HttpPost]


        public async Task<IActionResult> ObrisiMeni([FromBody] int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            // Provjera da li je korisnik administrator u bazi podataka
            var admin = _dbContext.Administrator
                          .Any(a => a.Id == korisnickiNalog.Id);


            if (admin == null)
            { Console.WriteLine("admin je "+ admin); 
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            }



            var meni = _dbContext.MeniStavka.Find(id);

            _dbContext.MeniStavka.Remove(meni);

            await _dbContext.SaveChangesAsync();
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
        public async Task<List<MeniGetKategorijaVM>> GetPoKategoriji(string nazivKategorije)
        {
            return await _dbContext.MeniStavka
         .Where(ms => ms.Kategorija.Naziv == nazivKategorije)
         .Select(ms => new MeniGetKategorijaVM
         {
             id = ms.Id,
             naziv = ms.Naziv,
             opis = ms.Opis,
             cijena = ms.Cijena,
             slika = ms.Slika,
             izdvojeno = ms.Izdvojeno,
             snizenaCijena = ms.SnizenaCijena,
             ocjena = ms.Ocjena,
             nazivKategorije = ms.Kategorija.Naziv
         })
         .ToListAsync();
        }



    }
}