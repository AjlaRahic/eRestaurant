using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class Dostava
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumIsporuke { get; set; }
        [ForeignKey("RestoranId")]
        public int RestoranId { get; set; }
        public Restoran Restoran { get; set; }

        [ForeignKey("UposlenikId")]
        public int UposlenikId { get; set; }
        public Uposlenik Uposlenik { get; set; }
        [ForeignKey("DostavnoVoziloId")]
        public int DostavnoVoziloId { get; set; }
        public DostavnoVozilo DostavnoVozilo { get; set; }
    }
}
