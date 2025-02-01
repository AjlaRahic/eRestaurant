import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {MyAuthInfo} from "./dto/my-auth-info";
import {LoginTokenDto} from './dto/login-token-dto';

@Injectable({providedIn: 'root'})
export class MyAuthService {
  constructor(private httpClient: HttpClient) {
  }

  getMyAuthInfo(): { id: number; ime: string; prezime: string; korisnickoIme: string } | null {
    const token = this.getLoginToken();
    return token?.user ?? null;  // Pristupamo user objektu
  }


  isAdmin(): boolean {
    const user = this.getMyAuthInfo();
    return user?.ime === 'admin';  // Proverava da li je korisnik admin na osnovu nekog svojstva, npr. ime
  }



  setLoggedInUser(x: LoginTokenDto | null) {
    if (x == null) {
      window.localStorage.setItem("my-auth-token", '');
    } else {
      window.localStorage.setItem("my-auth-token", JSON.stringify(x));
    }
  }


  getLoginToken(): { token: string; user: { id: number; ime: string; prezime: string; korisnickoIme: string } } | null {
    const tokenString = window.localStorage.getItem("my-auth-token") ?? "";
    try {
      const token = JSON.parse(tokenString);
      console.log('Parsiran token:', token); // Provera u konzoli
      return token;
    } catch (e) {
      console.error('Greška pri parsiranju tokena:', e);
      return null;
    }
  }



}
