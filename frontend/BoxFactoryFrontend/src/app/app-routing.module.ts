import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { BoxComponent } from './components/box/box.component';
import { BoxListComponent } from './components/box-list/box-list.component';
import { BoxCreateComponent } from './components/box-create/box-create.component';


const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then( m => m.HomePageModule)
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'box',
    component: BoxCreateComponent
  },
  {
    path: 'boxes',
    component: BoxListComponent
  },

  ]
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
