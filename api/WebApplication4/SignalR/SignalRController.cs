using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using WebApplication4.Migrations;
using WebApplication4.SignalR;
namespace WebApplication4.SignalR
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<PorukeHub> _porukeHub;

        

        public SignalRController(ApplicationDbContext dbContext, IHubContext<PorukeHub> porukeHub)
        {
            this._dbContext = dbContext;
            _porukeHub = porukeHub;
        }
        [HttpGet]
        public async Task<ActionResult> PosaljiTrenutnoVrijeme()
        {

            
            
            string p = "Trenutno vrijeme je: " + DateTime.Now;

            var novaPoruka = new Poruke
            {
                Tekst = p,
                VrijemeSlanja = DateTime.Now
            };

            _dbContext.Poruke.Add(novaPoruka);
            await _dbContext.SaveChangesAsync();
            await _porukeHub.Clients.All.SendAsync("slanje_poruke1", p);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> PosaljiPoruku(string p)
        {

            var novaPoruka = new Poruke
            {
                Tekst = p,
                VrijemeSlanja = DateTime.Now
            };

            _dbContext.Poruke.Add(novaPoruka);
            await _dbContext.SaveChangesAsync();
            await _porukeHub.Clients.All.SendAsync("slanje_poruke2", p);
            return Ok();
        }

       
    }
}