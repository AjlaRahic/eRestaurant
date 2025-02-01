import { Component, AfterViewInit, ViewChild, ElementRef, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Log } from './view-models/log-vm';
import { Chart } from 'chart.js/auto'; // Uključite Chart.js

import { MojConfig } from '../moj-config';

@Component({
  selector: 'app-logactivity',
  templateUrl: './logactivity.component.html',
  styleUrls: ['./logactivity.component.css']
})
export class LogactivityComponent implements OnInit, AfterViewInit {
  @ViewChild('chartCanvas') chartCanvas!: ElementRef; // Referenca na canvas element za graf

  // Promenljive za graf
  chartLabels: string[] = [];
  chartData: number[] = [];
  isChartVisible = false;
  logs: Log[] = [];
  startDate: string = '';
  endDate: string = '';
  errorMessage: string = '';

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {}

  // Funkcija za preuzimanje logova sa servera
  fetchLogs(startDate: string, endDate: string): void {
    if (!startDate || !endDate) {
      console.error('Unesite oba datuma!');
      return;
    }

    const start = new Date(startDate).toISOString().split('T')[0];
    const end = new Date(endDate).toISOString().split('T')[0];

    const url = `${MojConfig.adresa_servera}Log/GetLogs?startDate=${start}&endDate=${end}`;

    // Preuzimanje podataka sa servera
    this.httpClient.get<Log[]>(url, MojConfig.http_opcije()).subscribe(
      (response) => {
        this.logs = response;
        console.log("Podaci sa servera:", this.logs);

        // Procesiranje logova za graf
        this.processLogsForChart();

        // Kreiranje grafa
        this.showChart();
      },
      (error) => {
        console.error('Greška pri dohvaćanju podataka:', error);
        this.errorMessage = 'Greška pri dohvaćanju podataka. Pokušajte ponovo.';
      }
    );
  }

  ngAfterViewInit() {}

  // Funkcija koja procesira logove i priprema podatke za graf
  processLogsForChart(): void {
    this.chartLabels = this.logs.map(log => log.vrijeme); // Popunjavanje oznaka za x os
    this.chartData = this.logs.map(log => log.brojAktivnosti); // Popunjavanje podataka za y os

    console.log('chartLabels:', this.chartLabels);
    console.log('chartData:', this.chartData);
  }

  // Funkcija za kreiranje grafa
  createChart(): void {
    if (this.chartCanvas) {
      // Kreiranje stupčastog grafa (bar chart)
      new Chart(this.chartCanvas.nativeElement, {
        type: 'bar', // Promenite 'line' u 'bar' za stupčasti grafikon
        data: {
          labels: this.chartLabels, // Oznake za x os (datumi)
          datasets: [
            {
              label: 'Broj aktivnosti', // Naziv linije (u ovom slučaju "Broj aktivnosti")
              data: this.chartData, // Podaci za y os
              backgroundColor: 'rgba(75, 192, 192, 0.2)', // Boja stubaca
              borderColor: 'rgba(75, 192, 192, 1)', // Boja ivica stubaca
              borderWidth: 1 // Debljina ivice stubaca
            }
          ]
        },
        options: {
          responsive: true, // Graf se prilagođava veličini ekrana
          scales: {
            x: {
              title: {
                display: true,
                text: 'Datum' // Naziv x ose
              }
            },
            y: {
              title: {
                display: true,
                text: 'Broj aktivnosti' // Naziv y ose
              },
              beginAtZero: true // Početak y ose na nuli
            }
          }
        }
      });
    }
  }

  // Funkcija koja prikazuje graf
  showChart(): void {
    this.isChartVisible = true; // Postavljanje vidljivosti grafa
    this.createChart(); // Kreiranje grafa
  }
}
