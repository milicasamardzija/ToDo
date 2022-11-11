import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToDoListComponent } from './to-do-list/to-do-list.component';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { AssignmentDetailsComponent } from './assignment-details/assignment-details.component';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { PaginationComponent } from './pagination/pagination.component';
import { StepDetailsComponent } from './step-details/step-details.component';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AssignmentAddNewComponent } from './assignment-add-new/assignment-add-new.component'; 
import { MsalBroadcastService, MsalGuard, MsalInterceptor, MsalModule, MsalService } from '@azure/msal-angular';
import { PublicClientApplication, BrowserCacheLocation, InteractionType } from '@azure/msal-browser';
import { MatToolbarModule } from '@angular/material/toolbar';

@NgModule({
  declarations: [
    AppComponent,
    ToDoListComponent,
    AssignmentDetailsComponent,
    PaginationComponent,
    StepDetailsComponent,
    AssignmentAddNewComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatCardModule,
    MatButtonModule,
    FormsModule,
    NgxPaginationModule,
    HttpClientModule,
    MatInputModule,
    MatCheckboxModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatDatepickerModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    MatFormFieldModule,
    MatToolbarModule,
    MsalModule.forRoot(new PublicClientApplication({
      auth: {
        clientId: '33e5695b-7e44-4ac2-87fa-6ec31a06a42e',
        authority: 'https://login.microsoftonline.com/common',
        redirectUri: 'http://localhost:4200',
      },
      cache: {
        cacheLocation: BrowserCacheLocation.LocalStorage,
        storeAuthStateInCookie: false,
      }
    }), {
      // Guard Configuration
      interactionType: InteractionType.Redirect, 
    }, {
       // Interceptor Configuration
      interactionType: InteractionType.Redirect,
      protectedResourceMap: new Map([
        [ 'https://localhost:7153/', ['api://33e5695b-7e44-4ac2-87fa-6ec31a06a42e/user.read']]
      ])
    })
  ],
  bootstrap: [AppComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ]
})
export class AppModule { }
