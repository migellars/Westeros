import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then((_) => _.AuthModule),
  },
  {
    path: '',
    loadChildren: () => import('./modules/home/home.module').then((_) => _.HomeModule),
  },
  {
    path: 'author',
    loadChildren: () => import('./modules/author/author.module').then((_) => _.AuthorModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
