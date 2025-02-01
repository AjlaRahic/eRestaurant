using Microsoft.EntityFrameworkCore;
using WebApplication4.EntityModels;

namespace WebApplication4.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<DnevniMeni> DnevniMeni { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Dostava> Dostava { get; set; }
        
        public DbSet<DostavnoVozilo> DostavnoVozilo { get; set; }
        public DbSet<NarudzbaStavka> NarudzbaStavka { get; set; }
        public DbSet<KorpaStavke> KorpaStavke { get; set; }
        public DbSet<LogKretanjeSistem> LogKretanjeSistem { get; set; }
        public DbSet<LogKretanjePoSistemu> LogKretanjePoSistemu { get; set; }
        public DbSet<MeniStavka> MeniStavka{ get; set; }
        public DbSet<Notifikacije> Notifikacije { get; set; }
        public DbSet<Obavijesti> Obavijesti { get; set; }
       
        public DbSet<Restoran> Restorani { get; set; }
        public DbSet<Poruke> Poruke { get; set; }
        public DbSet<StatusNarudzbe> StatusNarudzbe { get; set; }
        public DbSet<Uposlenik> Uposlenici { get; set; }
        public DbSet<OmiljenaStavka>OmiljenaStavka { get; set; }
        public DbSet<OnlineNarudzba> OnlineNarudzba { get; set; }
        public DbSet<Ponuda> Ponuda { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<UlazStavka> UlazStavka { get; set; }
        public DbSet<UlazUSkladiste> UlazUSkladiste { get; set; }
        public DbSet<UposlenikObavijesti> UposlenikObavijesti { get; set; }
        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

    }
}
