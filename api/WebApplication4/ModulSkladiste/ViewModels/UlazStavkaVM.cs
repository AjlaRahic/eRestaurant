using System.ComponentModel.DataAnnotations.Schema;
using WebApplication4.EntityModels;

namespace WebApplication4.ModulSkladiste.ViewModels
{
    public class UlazStavkaVM
    {
        public int Id { get; set; }
        public string Kolicina { get; set; }
     
        
        public int UlazUSkladisteId { get; set; }
       
        public int MeniId { get; set; }
    }
}
