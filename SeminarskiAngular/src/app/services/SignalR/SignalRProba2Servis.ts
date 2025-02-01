import {Injectable} from "@angular/core";
import * as signalR from "@microsoft/signalr"
import {MojConfig} from '../../moj-config';

@Injectable({
  providedIn:"root"
})
export class SignalRProba2Servis{

  public ime_prezime : string = "";
  connection?:signalR.HubConnection | null;

  otvoriKanalWebSocket(){
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7147/poruke-hub-putanja')
      .build();

    this.connection.on('PosaljiPoruku',(p:string)=>{
      this.ime_prezime = p;
    });

    this.connection.start().then(()=>{
      console.log("Otvoren kanal WS.");
    });
  }

  posaljiImePrezime(){
    this.connection?.invoke("ProslijediPoruku", this.ime_prezime)
  }

}
