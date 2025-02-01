using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class Notifikacije
    {
        [Key]
        public int Id { get; set; }
        public string Poruka { get; set; }
        [ForeignKey("OnlineGostId")]
        public Korisnik OnlineGost { get; set; }
        public int OnlineGostId { get; set; }
    }
}
