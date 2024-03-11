import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventosComponent } from './components/eventos/eventos.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { DashBoardComponent } from './components/dash-board/dash-board.component';

const routes: Routes = [
  {
    path: 'eventos',
    component: EventosComponent
  },
  {
    path: 'palestrantes',
    component: PalestrantesComponent
  },
  {
    path: 'contatos',
    component: ContatosComponent
  },
  {
    path: 'perfil',
    component: PerfilComponent
  },
  {
    path: 'dash-board',
    component: DashBoardComponent
  },
  {
    path: '',
    redirectTo: 'dash-board',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'dash-board',
    pathMatch: 'full'
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
