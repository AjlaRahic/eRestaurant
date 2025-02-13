import {Injectable} from "@angular/core";
import * as signalR from "@microsoft/signalr"
import {MojConfig} from '../../moj-config';

@Injectable({
  providedIn:"root"
})
export class SignalRProba1Servis{

  public poruka1 : string = "Welcome to the chat!";
  public poruka2 : string = "It's nice to see you!";


  otvoriKanalWebSocket(){
    var connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7147/poruke-hub-putanja')
      .build();

    connection.on('slanje_poruke1',(p:string)=>{
      this.poruka1 = p;
    });

    connection.on('slanje_poruke2',(p:string)=>{
      this.poruka2= p;
    });

    connection.start().then(()=>{
      console.log("Otvoren kanal WS.");
    });
  }

}
