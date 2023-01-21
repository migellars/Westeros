import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorDashboardComponent } from './author-dashboard/author-dashboard.component';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { EditAuthorComponent } from './edit-author/edit-author.component';

const routes: Routes = [
  { path: "dashboard", component: AuthorDashboardComponent },
  { path: "create", component: CreateAuthorComponent },
  { path: "edit/:id", component: EditAuthorComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }
