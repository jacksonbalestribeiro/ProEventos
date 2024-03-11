import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../../sevices/evento.service';
import { Evento } from '../../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg: number = 150;
  public marginImg: number = 2;
  public showImg: boolean = false;
  private _filtroLista: string = '';
  modalRef?: BsModalRef;
  message?: string;

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  constructor(
    private eventoSevice: EventoService,
    private modalService: BsModalService,
    private toastrService: ToastrService,
    private spinner: NgxSpinnerService
  ){

  };

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  };

  public alterarImg(): void{
    this.showImg = !this.showImg;
  }

  public getEventos(): void {

    this.eventoSevice.getEventos().subscribe({
      next:(eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      },
      error:(error: any) => {
        this.spinner.hide();
        this.toastrService.error('Erro ao buscar eventos!', 'Erro!');
      },
      complete:() => {
        this.spinner.hide();
      }
    });
  }


  public filtrarEventos(filtroPor: string): Evento[] {
    filtroPor = filtroPor.toLocaleLowerCase();
    return this.eventos.filter(
      ( evento: any) =>
      evento.tema.toLocaleLowerCase().indexOf(filtroPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtroPor) !== -1
    );
  }

  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.toastrService.success('Evento deletado com sucesso!', 'Deletado!');
    this.modalRef?.hide();
  }

  decline(): void {
    this.toastrService.error('Ação não realizada!');
    this.modalRef?.hide();
  }
}
