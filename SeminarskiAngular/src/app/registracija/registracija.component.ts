import {Component, OnInit} from '@angular/core';
import {Login} from "../login/view-models/login-vm";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Registracija} from "./view-models/registracija-vm";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})

export class RegistracijaComponent implements OnInit {
  registracija : Registracija = new Registracija();

  gradovi: any;
  sifra:string;
  prijava : Login = new Login();

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  loginInformacije : LoginInformacije | null = null;
  constructor(private httpKlijent : HttpClient, private router : Router) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }


  ngOnInit(): void {
 this.registracija=new Registracija();
    this.getAllGradovi();
  }

   private getAllGradovi() {
     this.httpKlijent.get(MojConfig.adresa_servera+ "Grad/GetAll", MojConfig.http_opcije()).subscribe(x=>{
       this.gradovi = x;
     });
  }


  registracijaPodataka() {
    if (this.registracija.lozinka == this.sifra && this.validirajFormu()) {
      this.registracija.gradId = parseInt(this.registracija.gradId.toString());

      this.httpKlijent.post(MojConfig.adresa_servera + "Administratori/Dodaj", this.registracija)
        .subscribe((result: any) => {
          alert("Uspješno ste se registrovali");
          //this.router.navigateByUrl("/home");

          this.prijava.korisnickoIme = this.registracija.korisnickoIme;
          this.prijava.lozinka = this.registracija.lozinka;

          this.httpKlijent.post(MojConfig.adresa_servera + "Autentifikacija/Login", this.prijava)
            .subscribe((response: any) => {
              localStorage.setItem("autentifikacija-token", JSON.stringify(response));
              this.router.navigateByUrl("/home");
            });
        });
    } else {
      this.prikaziObavjestenje(
        "Neadekvatno ispunjena forma za registraciju",
        "Molimo ispunite sva obavezna polja ili ispravno unesite lozinku, pa ponovo pokušajte"
      );
    }
  }







  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }

  private validirajFormu() {
    var osnovneInformacije : boolean = this.registracija.korisnickoIme != null && this.registracija.korisnickoIme?.length > 0
      && this.registracija.lozinka != null && this.registracija.lozinka?.length > 0
      && this.registracija.ime != null && this.registracija.ime?.length > 0
      && this.registracija.prezime != null && this.registracija.prezime?.length > 0
      && this.registracija.email != null && this.registracija.email?.length > 0;

    return osnovneInformacije ;
  }
  provjeriPolje(polje: any) {
    if (polje.invalid && (polje.dirty || polje.touched)){
      if (polje.errors?.['required']){
        return 'Niste popunili ovo polje!';
      }
      else {
        return '';
      }
    }
    return 'Obavezno polje za unos';
  }
  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }



}
