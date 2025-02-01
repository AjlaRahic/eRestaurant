using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.EntityModels
{
    public class LogKretanjeSistem
    {
        public int Id { get; set; }
        public string? QueryPath { get; set; }
        public string? PostData { get; set; }
        public DateTime Vrijeme { get; set; }
        public string? IpAdresa { get; set; }
        public string? ExceptionMessage { get; set; }
        public bool isException { get; set; }
        [ForeignKey(nameof(Administrator))]
        public int AdministratorId { get; set; }
        public Administrator? Administrator { get; set; }
    }
}
