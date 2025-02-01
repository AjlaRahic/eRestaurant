import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { RegistracijaComponent } from './registracija/registracija.component';
import { MeniComponent } from './meni/meni.component';

import { KorisniciComponent } from './korisnici/korisnici.component';
import { SkladisteComponent } from './skladiste/skladiste.component';
import { HomeComponent } from './home/home.component';
import {AutorizacijaLoginProvjera} from './_guards/autorizacija-login-provjera.service';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { LoginComponent } from './login/login.component';
import {PregledulazaComponent} from './skladiste/pregledulaza/pregledulaza.component';
import {LogactivityComponent} from './logactivity/logactivity.component';
import {ChatComponent} from './chat/chat.component';
import { NgChartsConfiguration } from 'ng2-charts';
import { BaseChartDirective } from 'ng2-charts';
import {AdministratoriComponent} from './administratori/administratori.component';
//import {RestoraniComponent} from './restorani/restorani.component';

const routes: Routes = [
  // Prazna ruta preusmjerava na 'home'
  { path: 'home', component: HomeComponent}, // Ruta za Home komponentu
  { path: 'korisnici', component: KorisniciComponent}, // Ruta za About komponentu
  {path: 'pregledulaza', component: PregledulazaComponent},
  {path: 'registracija', component: RegistracijaComponent},
  {path: 'login', component: LoginComponent},
  {path: 'skladiste', component: SkladisteComponent},
  {path: 'app', component: AppComponent},
  {path: 'meni', component: MeniComponent,canActivate:[AutorizacijaLoginProvjera]},
  {path: 'log', component: LogactivityComponent},
  {path: 'chat', component: ChatComponent},

];


@NgModule({
  declarations: [
    AppComponent,
    RegistracijaComponent,
    MeniComponent,
    KorisniciComponent,
    SkladisteComponent,
    HomeComponent,
    LoginComponent,
    PregledulazaComponent,
    LogactivityComponent,
    ChatComponent,
    AdministratoriComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),




  ],

  providers: [
    AutorizacijaLoginProvjera,
     ],
  bootstrap: [AppComponent]
})
export class AppModule { }
