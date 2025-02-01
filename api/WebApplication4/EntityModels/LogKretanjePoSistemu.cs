using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class LogKretanjePoSistemu
    {
        [Key]
        public int Id { get; set; }
        public string QueryPath { get; set; }
        public string PostData { get; set; }
        public DateTime Vrijeme { get; set; }
        public string IpAdresa { get; set; }
        public string ExceptionMessage { get; set; }
        public bool isException { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
