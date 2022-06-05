import {Component} from '@angular/core';
import {TokenService} from "../services/tokens/token.service";
import {Role} from "../models/users/role.enum";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [],
  providers: [],
})
export class AppComponent {

  constructor(public _token: TokenService) {
  }

  get isStudent(): boolean {
    return this._token.role == Role.student;
  }

  get isTutor(): boolean {
    return this._token.role == Role.tutor;
  }

  get isLogin(): boolean {
    return this._token.isJwtTokenExists();
  }

  logout() {
    this._token.deleteJwtToken();
  }
}
