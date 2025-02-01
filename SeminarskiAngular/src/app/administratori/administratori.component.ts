import { Component } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import {MojConfig} from '../moj-config';
import {HttpClient} from '@angular/common/http';



@Component({
  selector: 'app-administratori',
  standalone: false,

  templateUrl: './administratori.component.html',
  styleUrl: './administratori.component.css'
})
export class AdministratoriComponent {
  filter_ime_prezime: boolean;
  ime_prezime: string = '';
  editadmin: any;
  slikaadmin: any;
 gradovi:any;
 administratori: any;
 odabranaSlika: File | null = null;
 constructor(private httpClient: HttpClient,private router: Router) {
  }

  ngOnInit() {
 this.fetchAdministratori();
    this.ucitajGradove();

  }

  fetchAdministratori() {
    this.httpClient.get(MojConfig.adresa_servera+ "Administrator/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.administratori = x;
    });
    }
  FiltrirajAdministratore() {
    if(this.ime_prezime == ""){
      return this.administratori;
    }

    return this.administratori.filter((x:any)=> (!this.filter_ime_prezime || (x.ime + ' '+ x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase()) ||  (x.prezime + ' '+ x.ime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())        )


    );
  }

  Obrisi(id:any) {
    this.httpClient.post(MojConfig.adresa_servera + "Administrator/Obrisi",id, MojConfig.http_opcije()).subscribe((response : any)=> {

        this.fetchAdministratori();

      },
      (error) => {
        console.error('Greška prilikom brisanja korisnika:', error);
        alert('Došlo je do greske prilikom brisanja. Pokusaj kasnije.');
      });}

  private ucitajGradove() {
    this.httpClient.get(MojConfig.adresa_servera+ "Grad/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.gradovi = x;
    });
  }


  otvoriFormuZaSliku(admin: any) {
  this.slikaadmin = admin;
  }

  zatvoriFormuZaSliku() {
    this.slikaadmin = null;
    this.odabranaSlika = null;
  }

  dodajSliku() {
    if (!this.odabranaSlika) {
      console.error('Nije odabrana slika.');
      alert('Molimo odaberite sliku pre nego što nastavite.');
      return;
    }

    const formData = new FormData();
    formData.append('slika', this.odabranaSlika, this.odabranaSlika.name); // Dodaj sliku u formData

    const id = this.slikaadmin.id; // ID korisnika
    const url = `https://localhost:7147/Administrator/DodajSliku?id=${id}`; // Dodaj ID u query parametar

    this.httpClient.post(url, formData).subscribe({
      next: (response) => {
        console.log('Uspešno dodata slika:', response);
        alert('Slika je uspešno dodata.');
        this.ngOnInit();
      },
      error: (err) => {
        console.error('Greška pri dodavanju slike:', err);
        alert('Greška: ' + err.error);
      },
    });}

  onFileSelected(event: any) {
    this.odabranaSlika = event.target.files[0];
  }



  EditujAdmina() {
    const korisnikZaSlanje = {
      id: this.editadmin.id,
      ime: this.editadmin.ime,
      prezime: this.editadmin.prezime,
      email: this.editadmin.email,
      adresaStanovanja: this.editadmin.adresa,  // Paziti na naziv polja!
      brojTelefona: this.editadmin.brojTelefona,
      korisnickoIme: this.editadmin.korisnickoIme,
      lozinka: this.editadmin.lozinka,  // Ako je obavezna
      gradId: this.editadmin.gradId
    };
    this.httpClient.post(MojConfig.adresa_servera + "Administrator/Dodaj", korisnikZaSlanje)
      .subscribe({
        next: () => alert("Uspješno ste editovali"),
        error: err => console.error("Greška API-ja:", err.error.errors)
      });this.editadmin=null;
  }
}
