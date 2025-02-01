import {Component, NgIterable} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {MojConfig} from '../moj-config';
import {LoginInformacije} from '../_helpers/login-informacije';
import {AutentifikacijaHelper} from '../_helpers/autentifikacija-helper';
import {Korisnik} from '../korisnici/korisnik-model';
import {MeniStavka} from './view-models/menistavka-vm';
import {Stavka} from './view-models/stavka';
import {MeniGrupa} from './view-models/menigrupa-vm';


export class Meni {
  naziv: string
  opis: string
  cijena:number

  izdvojeno:boolean
  snizenacijena:number
  ocjena:number
  kategorijaid:number
}

@Component({
  selector: 'app-meni',
  standalone: false,

  templateUrl: './meni.component.html',
  styleUrl: './meni.component.css'
})
export class MeniComponent {
  meniGrupe : MeniGrupa[];

  meni: Meni=new Meni();
  id:any;

  stavke: any;
  editstavka: any;
  slikastavka: any;
  novastavka: any;
  odabranaKategorija: string | null=null;
  odabranaSlika: File | null = null;
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpClient: HttpClient,private router: Router) {
  }
  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  ngOnInit() {
    this.ucitajStavke();
    this.ucitajKategorije();

  }






  Obrisi(id:any) {
    this.httpClient.post(MojConfig.adresa_servera + "Meni/ObrisiMeni", id, MojConfig.http_opcije())
      .subscribe((x: any) => {

    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = "Vaša stavka na meni je uspješno obrisana";
    this.obavjestenjeSadrzaj = "Uspješno ste obrisali odabranu stavku";
    this.ucitajStavke()  });
  }

  EditujStavku() {
    this.httpClient.post(MojConfig.adresa_servera + 'Meni/AddStavka', this.editstavka, MojConfig.http_opcije())
      .subscribe(
        (response) => {
          console.log("Odgovor API-ja: ", response); // Prikazuje odgovor
          alert("Stavka je uspjesno editovana");
          this.editstavka = null;
          this.ngOnInit();
        },
        (error) => {
          console.error("Greška API-ja: ", error); // Prikazuje grešku
          alert("Nije dodano, Pokusaj opet kasnije");
        }
      );}



  getNazivKategorije(kategorijaid: number): string {
    const kategorija = this.meniGrupe.find(k => k.id == kategorijaid);
    return kategorija ? kategorija.naziv : "";
  }

  DodajNovu(){
    this.novastavka = {
      Naziv: '',
      Opis: '',
      Cijena: 0,
      Izdvojeno: false,
      SnizenaCijena: 0,
      Ocjena: 0,
      KategorijaId: 0
    };
  }


  ucitajStavke() {
    this.httpClient.get(MojConfig.adresa_servera + "Meni/GetAll", MojConfig.http_opcije()).subscribe(x => {
      this.stavke = x;
      console.log("podaci sa servera:",this.stavke);


      // Dodaj baseUrl na putanju slike
      this.stavke.forEach((meni: MeniStavka) => {
        if (meni.slika) {
          meni.slika = MojConfig.baseUrl + meni.slika; // Dodaj baseUrl na početak putanje slike
        }
      });
      return this.stavke;
    });
  }

  ucitajKategorije() {
    this.httpClient.get(MojConfig.adresa_servera + "Kategorija/GetALL").subscribe((result : any)=>{
      this.meniGrupe = result;
    })
  }





  DodajNovuStavku(){   this.httpClient.post(MojConfig.adresa_servera + 'Meni/AddStavka', this.novastavka, MojConfig.http_opcije())
      .subscribe(
        (response) => {
          console.log("Odgovor API-ja: ", response); // Prikazuje odgovor
          alert("Stavka je uspjesno dodana");
          this.novastavka = null;
          this.ngOnInit();
        },
        (error) => {
          console.error("Greška API-ja: ", error); // Prikazuje grešku
          alert("Nije dodano, Pokusaj opet kasnije");
        }
      );}
  filtrirajPoKategoriji(nazivKategorije: string | null) {
    if (!nazivKategorije || nazivKategorije == "null") {
      this.ucitajStavke();
      return;
    }
    const url = MojConfig.adresa_servera + "Meni/GetPoKategoriji?nazivKategorije=" + nazivKategorije;
    this.httpClient.get<MeniStavka[]>(url).subscribe(
      (data) => {
        this.stavke = data.map((meni: MeniStavka) => {
          if (meni.slika) {
            meni.slika = MojConfig.baseUrl + meni.slika;
          }
          return meni;
        });
        console.log('Filtrirane stavke:', this.stavke);
      },
      (error) => {
        console.error('Greška prilikom filtriranja:', error);
        alert('Došlo je do greške pri filtriranju.');
      }
    );
  }


  otvoriFormuZaSliku(stavka: any) {
    this.slikastavka = stavka;
  }

  zatvoriFormuZaSliku() {
    this.slikastavka = null;
    this.odabranaSlika = null;
  }

  // Handle file selection

  onFileSelected(event: any) {
    this.odabranaSlika = event.target.files[0];
  }


  dodajSliku() {
    if (!this.odabranaSlika) {
      console.error('Nije odabrana slika.');
      alert('Molimo odaberite sliku pre nego što nastavite.');
      return;
    }
    const formData = new FormData();
    formData.append('slika', this.odabranaSlika, this.odabranaSlika.name);
    const id = this.slikastavka.id;
    const url = `https://localhost:7147/Meni/AddMeniSlika?id=${id}`;
    this.httpClient.post(url, formData).subscribe({
      next: (response) => {
        console.log('Uspešno dodata slika:', response);
        alert('Slika je uspešno dodata.');
        this.slikastavka = null;
        this.ngOnInit();
      },
      error: (err) => {
        console.error('Greška pri dodavanju slike:', err);
        alert('Greška: ' + err.error);
      },
    });}



  protected readonly toString = toString;
}
