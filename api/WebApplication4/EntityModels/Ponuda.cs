using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class Ponuda
    {
        [Key]
        public int Id { get; set; }
        public string DostupnaKolicina { get; set; }
        [ForeignKey("MeniId")]
        public MeniStavka Meni { get; set; }
        public int MeniId { get; set; }
        [ForeignKey("DnevniMeniId")]
        public DnevniMeni DnevniMeni { get; set; }
        public int? DnevniMeniId { get; set; }
    }
}
