using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.EntityModels
{
    [Table("Restorani")]
    public class Restoran
    {
        [Key]
        
        public int Id { get; set; }
        
        public string Adresa {  get; set; }
        public string RadnoVrijemeRadnimDanima { get; set; }
        public string RadnoVrijemeVikendom {  get; set; }
        public string Telefon {  get; set; }
        [ForeignKey ("GradId")]
        public int GradId { get; set; }
        public Grad Grad { get; set; }

    }
}
