import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {


  public eventos: any = [];
  public eventosFiltrados: any = [];
  public widthImg: number = 150;
  public marginImg: number = 2;
  public showImg: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  constructor(private http: HttpClient){};

  ngOnInit(): void {
    this.getEventos();
  };

  alterarImg(){
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = response;
      },
      error => console.log(error)
    )
  }

  filtrarEventos(filtroPor: string): any {
    filtroPor = filtroPor.toLocaleLowerCase();
    return this.eventos.filter(
      ( evento: any) =>
      evento.tema.toLocaleLowerCase().indexOf(filtroPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtroPor) !== -1
    );
  }
}
