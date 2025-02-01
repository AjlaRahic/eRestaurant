import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../moj-config';
import { Chart } from 'chart.js';
import { ulazi } from '../view-models/ulazi';
import { CategoryScale, LinearScale , LineElement, PointElement, Tooltip, Legend, LineController} from 'chart.js';
import{jsPDF} from 'jspdf';
import html2canvas from 'html2canvas'; // Ispravan način importiranja
  // Dodajemo html2canvas za snimanje grafikona

@Component({
  selector: 'app-pregledulaza',
  templateUrl: './pregledulaza.component.html',
  styleUrls: ['./pregledulaza.component.css']
})
export class PregledulazaComponent implements OnInit {
  ulazi: ulazi[] = [];
  startDate: string = '';
  endDate: string = '';

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {}

  fetchPodaci(startDate: string, endDate: string) {
    if (!startDate || !endDate) {
      console.error('Datumi nisu ispravno uneseni.');
      return;
    }
    const url = `${MojConfig.adresa_servera}Ulaz/GetUlaziPoPeriodu?startDate=${startDate}&endDate=${endDate}`;
    this.httpClient.get<ulazi[]>(url, MojConfig.http_opcije()).subscribe(
      (x) => {
        this.ulazi = x;
        console.log("Podaci sa servera:", this.ulazi);


        this.createChart();
      },
      (error) => {
        console.error('Greška pri dohvaćanju podataka:', error);
      }
    );
  }

  createChart() {
    if (!this.ulazi || this.ulazi.length === 0) {
      console.error('Nema podataka za prikazivanje grafikona.');
      return;
    }
    Chart.register(CategoryScale, LinearScale, LineElement, PointElement, Tooltip, Legend, LineController);
    const labels = this.ulazi.map(item => item.datum);
    const data = {
      labels: labels,
      datasets: [
        {
          label: 'Ukupna količina',
          data: this.ulazi.map(item => item.totalKolicina),
          borderColor: 'rgba(75, 192, 192, 1)',
          fill: false
        },
        {
          label: 'Ukupna cijena',
          data: this.ulazi.map(item => item.totalCijena),
          borderColor: 'rgba(153, 102, 255, 1)',
          fill: false
        }
      ]
    };
    const existingChart = Chart.getChart('myChart');
    if (existingChart) {
      existingChart.destroy();
    }
    new Chart('myChart', {
      type: 'line',
      data: data,
      options: {
        responsive: true,
        scales: {
          x: {
            type: 'category',
            title: { display: true, text: 'Datum' }
          },
          y: {
            type: 'linear',
            title: { display: true, text: 'Vrijednost' }
          }
        },
        plugins: {
          tooltip: {
            enabled: true
          },
          legend: {
            position: 'top'
          }
        }
      }
    });
  }
  generatePDF() {
    const doc = new jsPDF();
    doc.setFontSize(16);
    doc.text('Pregled ulaza robe po periodu', 20, 20);

    let yPosition = 40;
    doc.setFontSize(12);
    doc.text('Datum prijema | Ukupna količina | Ukupna cijena', 20, yPosition);
    yPosition += 10;
    this.ulazi.forEach(item => {
      doc.text(`${item.datum} | ${item.totalKolicina} | ${item.totalCijena}`, 20, yPosition);
      yPosition += 10;
    });
    const chartCanvas = document.getElementById('myChart') as HTMLCanvasElement;
    html2canvas(chartCanvas).then(canvas => {
      const imgData = canvas.toDataURL('image/png');
      doc.addImage(imgData, 'PNG', 20, yPosition, 170, 90);
      doc.save('Pregled_ulaza_roba.pdf');
    });
  }

}
