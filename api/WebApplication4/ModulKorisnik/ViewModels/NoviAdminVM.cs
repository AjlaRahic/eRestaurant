using System.ComponentModel.DataAnnotations.Schema;
using WebApplication4.EntityModels;

namespace WebApplication4.ModulKorisnik.ViewModels
{
    public class NoviAdminVM
    {
        public int id { get; set; }
        public DateTime datumKreiranja { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string? adresaStanovanja { get; set; }
        public string? email { get; set; }
        public string? adresa { get; set; }
        public string? brojTelefona { get; set; }
        public string? slika { get; set; }
        public string korisnickoIme { get; set; }
        public string? lozinka { get; set; }


        
        public int? gradId { get; set; }

    }
}
