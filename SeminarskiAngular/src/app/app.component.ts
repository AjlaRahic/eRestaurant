import { Component, OnInit } from '@angular/core';
import { MyAuthService } from './services/auth-services/my-auth.service';
import {Router} from '@angular/router';  // Uvoz MyAuthService
import { ChangeDetectorRef } from '@angular/core';
import {AutentifikacijaHelper} from './_helpers/autentifikacija-helper';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  userName: string | undefined;
  title: string;

  constructor(private authService: MyAuthService, private router: Router, private cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {

  }
}
