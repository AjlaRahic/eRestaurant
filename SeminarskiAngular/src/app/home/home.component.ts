import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {Router} from '@angular/router';
import {MyAuthService} from '../services/auth-services/my-auth.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  standalone:false
})
export class HomeComponent implements OnInit {
ime:string | undefined;
  constructor(private authService: MyAuthService, private router: Router) { }

  ngOnInit(): void {
    const token = this.authService.getLoginToken();
    if (token && token.user) {
      this.ime = token.user.korisnickoIme; // Pristup korisničkom imenu
      console.log('Korisničko ime:', this.ime); // Provera u konzoli
    } else {
      console.log('Token nije pronađen ili ne sadrži informacije o korisniku.');
      this.ime = 'gost'; // Fallback vrednost
    }}

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
