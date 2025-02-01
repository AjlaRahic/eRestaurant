using System.ComponentModel.DataAnnotations.Schema;
using WebApplication4.EntityModels;

namespace WebApplication4.ModulSkladiste.ViewModels
{
    public class UlazUSkladisteVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public float KolicinaUlaza { get; set; }
        public string ImeDobavljaca { get; set; }
        public DateTime DatumPrijema { get; set; }
        
     
    }
}
