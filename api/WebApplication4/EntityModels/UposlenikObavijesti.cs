using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class UposlenikObavijesti
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UposlenikId")]
        public Uposlenik Uposlenik { get; set; }
        public int UposlenikId { get; set; }
        public Obavijesti Obavijesti { get; set; }
        [ForeignKey("ObavijestiId")]
        public int ObavijestiId { get; set; }
    }
}
