using System.ComponentModel.DataAnnotations.Schema;
using WebApplication4.EntityModels;

namespace WebApplication4.ModulMeni.ViewModels
{
    public class AddMeniVM
    {
       public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
    

        public bool Izdvojeno { get; set; }
        public float SnizenaCijena { get; set; }
        public float Ocjena { get; set; }
        
        public int KategorijaId { get; set; }
       
    }
}
