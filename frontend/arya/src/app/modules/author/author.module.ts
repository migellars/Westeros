import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorRoutingModule } from './author-routing.module';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { EditAuthorComponent } from './edit-author/edit-author.component';
import { AuthorDashboardComponent } from './author-dashboard/author-dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CreateAuthorComponent,
    EditAuthorComponent,
    AuthorDashboardComponent
  ],
  imports: [
    CommonModule,
    AuthorRoutingModule,
    ReactiveFormsModule
  ]
})
export class AuthorModule { }
