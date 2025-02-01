namespace WebApplication4.ModulKorisnik.ViewModels
{
    public class NoviKorisnikVM
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        
        public string email { get; set; }
        public string adresaStanovanja { get; set; }
        public string brojTelefona { get; set; }
        public int? gradId { get; set; }
        public string korisnickoIme { get; set; }
        public string? lozinka { get; set; }
        
    }
}
