using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class DnevniMeni
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Cijena { get; set; }

    }
}
