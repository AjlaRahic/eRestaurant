using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.EntityModels
{
    [Table("Administrator")]
    public class Administrator:KorisnickiNalog
    {
  
        public DateTime DatumKreiranja { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string ?Email { get; set; }
        public string ?Adresa { get; set; }
        public string ?BrojTelefona { get; set; }
        public string? Slika { get; set; }



        [ForeignKey(nameof(Grad))]
        public int? GradId { get; set; }
        public Grad? Grad { get; set; }

    }
}
