import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { IAssignee } from './model/assignee';
import { UserService } from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  user : IAssignee = { email : "", name : ""};

  constructor(private msal: MsalService, private service: UserService) {
    this.setLogin();
  }

  ngOnInit(): void {
  }

  private setLogin() {
    const locallAccounts = this.msal.instance.getAllAccounts();
    const loggedIn = locallAccounts.length > 0;
    if (loggedIn) {
      this.msal.instance.setActiveAccount(locallAccounts[0]);
      this.service.getUser().subscribe(
        (response : any) => {
          this.user = response.result;
        }
      )
    }
  }

  logout() {
    this.msal.logout();
  }
}

