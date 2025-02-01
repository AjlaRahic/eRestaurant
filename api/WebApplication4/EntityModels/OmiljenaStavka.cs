using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.EntityModels
{
    public class OmiljenaStavka
    {
        public int ID { get; set; }
        [ForeignKey("KorisnikID")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey("MeniStavkaID")]
        public int MeniStavkaID { get; set; }
        public MeniStavka MeniStavka { get; set; }
    }
}
