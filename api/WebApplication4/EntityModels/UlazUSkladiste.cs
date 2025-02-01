using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class UlazUSkladiste
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }

        public float Cijena { get; set; }
        public float KolicinaUlaza { get; set; }
        public string ImeDobavljaca { get; set; }
        public DateTime DatumPrijema { get; set; }
      
    }
}
