using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    
    public class MeniStavka

    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public string? Slika { get; set; }
       
        public bool Izdvojeno { get; set; }
        public float SnizenaCijena { get; set; }
        public float Ocjena { get; set; }
        [ForeignKey("KategorijaId")]
        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }

    }
}
