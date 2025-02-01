import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MojConfig } from '../moj-config';
import { Registracija } from './view-models/registracija-vm';
import { Grad } from './view-models/grad-vm';

@Injectable({
  providedIn: 'root'
})
export class RegistracijaService {
  constructor(private http: HttpClient) {}

  getGradovi(): Observable<Grad[]> {
    return this.http.get<Grad[]>(`${MojConfig.adresa_servera}/Grad/GetAll/GetAllGrad`, MojConfig.http_opcije());
  }

  dodajKorisnika(registracija: Registracija): Observable<any> {
    return this.http.post(`${MojConfig.adresa_servera}/Korisnik/Dodaj/DodajKorisnika`, registracija, MojConfig.http_opcije());
  }

  login(korisnickoIme: string, lozinka: string): Observable<any> {
    return this.http.post(`${MojConfig.adresa_servera}/Autentifikacija/Login`, { korisnickoIme, lozinka }, MojConfig.http_opcije());
  }
}
