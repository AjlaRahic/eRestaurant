import { BehaviorSubject } from 'rxjs';
import { Component, OnInit } from '@angular/core';

import {HttpClient, HttpErrorResponse, HttpHeaders, HttpParams} from '@angular/common/http';
import { MojConfig } from '../moj-config';


import {Router} from '@angular/router';

import{Korisnik} from './korisnik-model';
// @ts-ignore
import {error} from '@angular/compiler-cli/src/transformers/util';
@Component({
  selector: 'app-korisnici',
  standalone: false,
  templateUrl: './korisnici.component.html',
  styleUrl: './korisnici.component.css',

})

export class KorisniciComponent implements OnInit {


  gradovi: any;
  filter_ime_prezime: boolean;
  korisnicipodaci: any;

  ime_prezime: string = '';



  novikorisnik:any;
  editkorisnik: any;
  slikakorisnik: any;
  odabranaSlika: File | null = null;



  constructor(private httpClient: HttpClient,private router: Router) {
  }

  ngOnInit() {
    this.fetchKorisnici();
    this.ucitajGradove();

  }

ucitajGradove(){
  this.httpClient.get(MojConfig.adresa_servera+ "Grad/GetAll", MojConfig.http_opcije()).subscribe(x=>{
    this.gradovi = x;
  });
}






  Obrisi(id: any) {
    this.httpClient.post(MojConfig.adresa_servera + "Korisnik/Obrisi",id, MojConfig.http_opcije()).subscribe((response : any)=> {

      this.fetchKorisnici();

    },
      (error) => {
        console.error('Greška prilikom brisanja korisnika:', error);
        alert('Došlo je do greske prilikom brisanja. Pokusaj kasnije.');
      });}



  EditujKorisnika() {
    console.log('Podaci poslati API-ju:', this.editkorisnik);
    const korisnikZaSlanje = {
      id: this.editkorisnik.id,
      ime: this.editkorisnik.ime,
      prezime: this.editkorisnik.prezime,
      email: this.editkorisnik.email,
      adresaStanovanja: this.editkorisnik.adresa,  // Paziti na naziv polja!
      brojTelefona: this.editkorisnik.brojTelefona,
      korisnickoIme: this.editkorisnik.korisnickoIme,
      lozinka: this.editkorisnik.lozinka,  // Ako je obavezna
      gradId: this.editkorisnik.gradId
    };
    this.httpClient.post(MojConfig.adresa_servera + "Korisnik/Dodaj", korisnikZaSlanje)
      .subscribe({
        next: () => alert("Uspješno ste editovali"),
        error: err => console.error("Greška API-ja:", err.error.errors)
      });this.editkorisnik=null;
  }





  otvoriFormuZaSliku(korisnik: any) {
    this.slikakorisnik = korisnik;
  }

  zatvoriFormuZaSliku() {
    this.slikakorisnik = null;
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
    formData.append('slika', this.odabranaSlika, this.odabranaSlika.name); // Dodaj sliku u formData

    const id = this.slikakorisnik.id; // ID korisnika
    const url = `https://localhost:7147/Korisnik/AddProfileImage?id=${id}`; // Dodaj ID u query parametar

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







   fetchKorisnici() {
    this.httpClient.get(MojConfig.adresa_servera + "Korisnik/GetAllKorisnike", MojConfig.http_opcije()).subscribe(x => {
      this.korisnicipodaci = x;
  console.log("podaci sa servera:",this.korisnicipodaci);
      // Dodaj baseUrl na putanju slike
      this.korisnicipodaci.forEach((korisnik: Korisnik) => {
        if (korisnik.slika) {
          korisnik.slika = MojConfig.baseUrl + korisnik.slika; // Dodaj baseUrl na početak putanje slike
        }
      });
      return this.korisnicipodaci;
    });
  }


  FiltrirajKorisnike() {
    if(this.ime_prezime == ""){
      return this.korisnicipodaci;
    }

    return this.korisnicipodaci.filter((x:any)=> (!this.filter_ime_prezime || (x.ime + ' '+ x.prezime).toLowerCase().
      startsWith(this.ime_prezime.toLowerCase()) ||  (x.prezime + ' '+ x.ime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())        )


    );
  }}


