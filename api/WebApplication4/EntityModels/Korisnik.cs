using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication4.EntityModels
{
    [Table("Korisnik")]
    public class Korisnik:KorisnickiNalog
    { 
        public string Ime { get; set; }
        public string Prezime { get; set; }
      
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string? Slika { get; set; }
       
       
      
        
        public int? GradId { get; set; }
        public Grad? Grad { get; set; }
    }

}
