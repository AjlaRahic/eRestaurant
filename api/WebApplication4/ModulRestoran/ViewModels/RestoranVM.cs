using System.ComponentModel.DataAnnotations.Schema;
using WebApplication4.EntityModels;

namespace WebApplication4.ModulRestoran.ViewModels
{
    public class RestoranVM
    {
        public int Id { get; set; }

        public string Adresa { get; set; }
        public string RadnoVrijemeRadnimDanima { get; set; }
        public string RadnoVrijemeVikendom { get; set; }
        public string Telefon { get; set; }
        
        public int GradId { get; set; }
        
    }
}
