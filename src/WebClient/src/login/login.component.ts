import {Component} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observer} from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router';
import {TokenService} from "../services/tokens/token.service";
import {UsersService} from "../services/api/users.service";

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: [],
  providers: [UsersService, HttpClient, TokenService]
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
        this.loginErrorHandle(response);
        console.log(response);
      },
      next: (next: any) => {
        this.jwtTokenSave(next.token);
        console.log(next);
      },
      complete: () => {
        this.errorMessage = "";
        this.navigateToPreviousPage();
      }
    }
    this._users.authenticate(this.username, this.password)
      .subscribe(observer);
  }

  private loginErrorHandle(response: HttpErrorResponse): void {
    if (response.status == 400) {
      this.errorMessage = response.error.message;
    }
  }

  private jwtTokenSave(jwtToken: string): void {
    this._token.jwtToken = jwtToken;
  }

  private navigateToPreviousPage(): void {
    const redirect = window.history.state.redirect ?? 'tests';
    this._router.navigate([redirect]);
  }
}
