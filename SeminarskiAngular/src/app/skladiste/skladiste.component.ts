import { Component } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {MojConfig} from '../moj-config';
import {noviulaz} from './view-models/skladistestavka-vm';
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-skladiste',
  standalone: false,
  templateUrl: './skladiste.component.html',
  styleUrl: './skladiste.component.css'
})




export class SkladisteComponent {


skladistestavke:any;
  noviulaz : any;

  editulaz: any;
  constructor(private httpClient: HttpClient,private router: Router) {
  }
  ngOnInit() {
    this.DohvatiStavke();


  }
  DohvatiStavke() {
    this.httpClient.get(MojConfig.adresa_servera+ "Ulaz/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.skladistestavke = x;
    });

  }

  NoviUlazUSkladiste() {
    this.noviulaz = {
      Naziv: '',
      ImeDobavljaca: '',
      Cijena: 0,
      KolicinaUlaza:0,
      DatumPrijena:new Date(),



    };
  }

  DodajStavku() {
    this.httpClient.post(MojConfig.adresa_servera + "Ulaz/DodajStavku", this.noviulaz, MojConfig.http_opcije())
      .subscribe((x: any) => {


        this.ngOnInit();
        this.noviulaz= null;
        porukaSuccess("Stavka je dodana");
      },error => {
        porukaError("Stavka nije dodana");
      });
  }

  ObrisiStavku(id:any) {
    this.httpClient.post(MojConfig.adresa_servera + "Ulaz/Obrisi", id, MojConfig.http_opcije())
      .subscribe((x: any) => {


        this.ngOnInit();
        porukaSuccess("Stavka je obrisana");
      },error => {
        porukaError("Stavka nije obrisan");
      });
  }

  EditujStavku() {
    this.httpClient.post(MojConfig.adresa_servera + "Ulaz/DodajStavku", this.editulaz, MojConfig.http_opcije())
      .subscribe((x: any) => {


        this.ngOnInit();
        this.editulaz= null;
        porukaSuccess("Stavka je uređena");
      },error => {
        porukaError("Stavka nije uređena");
      });
  }
}
