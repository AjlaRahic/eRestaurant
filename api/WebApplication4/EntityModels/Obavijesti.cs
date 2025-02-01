using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class Obavijesti
    {
        [Key]
        public int Id { get; set; }
        public string Poruka { get; set; }
        public DateTime Datum { get; set; }
        public IEnumerable<UposlenikObavijesti> Uposlenik { get; set; }
    }
}
