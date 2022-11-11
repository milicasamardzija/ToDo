import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { AssignmentDetailsComponent } from './assignment-details/assignment-details.component';

import { ToDoListComponent } from './to-do-list/to-do-list.component';

const routes: Routes = [
  { path: '', component: ToDoListComponent , canActivate: [ MsalGuard ]},
  { path: ':id', component: AssignmentDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
