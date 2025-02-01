using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using WebApplication4.Data;
using XAct;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using WebApplication4.Helper.AutentifikacijaAutorizacija;
using Microsoft.EntityFrameworkCore;
namespace WebApplication4.SignalR

{
    public class PorukeHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public PorukeHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProslijediPoruku(string p)
        {
            
            var novaPoruka = new Poruke
            {
                Tekst = p,
                VrijemeSlanja = DateTime.Now
            };
            Console.WriteLine("poruka poslana" + p);
            _dbContext.Poruke.Add(novaPoruka);
            await _dbContext.SaveChangesAsync();


            await Clients.Others.SendAsync("PosaljiPoruku", p);
        }

    }

}

    

