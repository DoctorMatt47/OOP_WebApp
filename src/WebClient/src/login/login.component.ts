import {Component} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observer} from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router';
import {TokenService} from "../services/tokens/token.service";
import {UsersService} from "../services/api/users.service";
import {environment} from "../environments/environment";
import {Role} from "../models/users/role.enum";

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: [],
  providers: [UsersService, HttpClient]
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = ''

  constructor(
    private _users: UsersService,
    private _token: TokenService,
    private _route: ActivatedRoute,
    private _router: Router
  ) {
  }

  login(): void {
    if (this.username == '') {
      this.errorMessage = 'Enter your username or email';
      return;
    }
    if (this.password == '') {
      this.errorMessage = 'Enter your password';
      return;
    }
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => {
        if (response.status == 400) this.errorMessage = response.error.message;
        if (!environment.production) console.log(response);
      },
      next: (next: { token: string, role: Role, username: string }) => {
        this._token.jwtToken = next.token;
        this._token.role = next.role;
        this._token.username = next.username;
        if (!environment.production) console.log(next);
      },
      complete: () => {
        this.errorMessage = "";
        this.navigateToPreviousPage();
      }
    }
    this._users.authenticate(this.username, this.password)
      .subscribe(observer);
  }
  private navigateToPreviousPage(): void {
    const redirect = window.history.state.redirect ?? 'tests';
    this._router.navigate([redirect]);
  }
}
