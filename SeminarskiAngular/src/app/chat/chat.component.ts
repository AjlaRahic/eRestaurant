import {SignalRProba1Servis} from "../../app/services/SignalR/SignalRProba1Servis";
import {Component, OnInit} from "@angular/core";

import{SignalRProba2Servis}from "../../app/services/SignalR/SignalRProba2Servis";


@Component({
  selector: 'app-root',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit{

  constructor(public probaServis1: SignalRProba1Servis, public probaServis2: SignalRProba2Servis) {
    probaServis1.otvoriKanalWebSocket();
    probaServis2.otvoriKanalWebSocket();
  }

  ngOnInit(): void {

  }
}
