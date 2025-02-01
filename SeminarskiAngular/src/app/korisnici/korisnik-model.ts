export interface Korisnik {
  ime: string;
  prezime: string;
  email: string;
  adresa: string;
  brojTelefona: string;
  slika: string;
  gradId: string | null;
  grad: string | null;
  id: number;
  korisnickoIme: string;
  isKorisnik: boolean;
  isAdministrator: boolean;
  isUposlenik: boolean;
}
