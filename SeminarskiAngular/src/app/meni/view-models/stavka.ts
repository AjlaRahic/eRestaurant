export class Stavka {
  id: number;
  naziv: string;
  opis: string;
  cijena: number;
  snizenaCijena?: number;
  ocjena: number;
  kategorijaId: number;
  izdvojeno: boolean;
  slika?: string;
}
