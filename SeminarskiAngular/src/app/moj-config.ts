import {HttpHeaders} from "@angular/common/http";
import {AutentifikacijaHelper} from "./_helpers/autentifikacija-helper";
import {AutentifikacijaToken} from "./_helpers/login-informacije";

export class MojConfig{
  static adresa_servera = "https://localhost:7147/";
  static baseUrl: string = 'https://localhost:7147/';
  static http_opcije= function (){

    let autentifikacijaToken:AutentifikacijaToken | null=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if (autentifikacijaToken!=null)
      mojtoken = autentifikacijaToken.vrijednost;
    return {
      headers: {
        'autentifikacija-token': mojtoken,
      }
    };
  }

}


