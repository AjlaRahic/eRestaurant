using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class UlazStavka
    {
        [Key]
        public int Id { get; set; }
        public string Kolicina { get; set; }
        [ForeignKey("UlazUSkaldisteId")]
        public UlazUSkladiste UlazUSkladiste { get; set; }
        public int UlazUSkladisteId { get; set; }
        

    }
}
