using System.ComponentModel.DataAnnotations;

namespace WebApplication4.EntityModels
{
    public class DostavnoVozilo
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Tip { get; set; }
    }
}
