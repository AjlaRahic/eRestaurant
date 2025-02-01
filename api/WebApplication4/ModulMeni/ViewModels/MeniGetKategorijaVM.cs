namespace WebApplication4.ModulMeni.ViewModels
{
    public class MeniGetKategorijaVM
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public float cijena { get; set; }
        public string slika { get; set; }
        public bool izdvojeno { get; set; }
        public float snizenaCijena { get; set; }
        public float ocjena { get; set; }
        public string nazivKategorije { get; set; }
        public List<IFormFile> MeniSlika { get; set; }
    }
}
