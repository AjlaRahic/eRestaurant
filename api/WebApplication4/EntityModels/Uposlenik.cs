using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.EntityModels
{
    [Table("Uposlenik")]
    public class Uposlenik:KorisnickiNalog
    {
        public string Slika { get; set; }
        public int ObavljeneNarudzbe { get; set; }
        public int AktivneNarudzbe { get; set; }



    }
}
