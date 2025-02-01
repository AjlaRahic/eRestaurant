using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.EntityModels
{
    
    public class Grad
    {
        [Key]
        public int Id { get; set; }
        public string Naziv {  get; set; }
    }
}
