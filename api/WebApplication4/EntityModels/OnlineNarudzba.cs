using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class OnlineNarudzba
    {
        [Key]
        public int ID { get; set; }
        public float Cijena { get; set; }
        public DateTime DatumNarucivanja { get; set; }
        public bool Zakljucena { get; set; }
        public int BrojStavki { get; set; }

        [ForeignKey("KorisnikID")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [ForeignKey("StatusNarudzbeID")]
        public int? StatusNarudzbeID { get; set; }
        public StatusNarudzbe StatusNarudzbe { get; set; }
        [ForeignKey("UposlenikID")]
        public int? UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }

    }
}
