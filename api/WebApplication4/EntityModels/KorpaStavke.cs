using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class KorpaStavke
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("MeniId")]
        public int MeniId { get; set; }
        public MeniStavka Meni { get; set; }
        public int Kolicina { get; set; }
        public string KorpaId { get; set; }
    }
}
