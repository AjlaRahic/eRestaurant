using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication4.EntityModels
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        public int Id { get; set; }
       public string KorisnickoIme { get; set; }
        [JsonIgnore]
        public string? Lozinka { get; set; }

       
        [JsonIgnore]
        public Korisnik? korisnik => this as Korisnik;
        [JsonIgnore]
        public Uposlenik? uposlenik => this as Uposlenik;
        [JsonIgnore]
        public Administrator? administrator => this as Administrator;
        public bool isKorisnik => korisnik != null;
        public bool isAdministrator => administrator != null;
        public bool isUposlenik => uposlenik != null;

    }
}
