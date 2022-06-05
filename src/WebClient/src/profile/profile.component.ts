import {Component} from "@angular/core";
import {TokenService} from "../services/tokens/token.service";
import {TestService} from "../services/api/test.service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Test} from "../models/tests/test.model";
import {Router} from "@angular/router";
import {Observer} from "rxjs";
import {Role} from "../models/users/role.enum";

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  providers: [TokenService, TestService, HttpClient]
})
export class ProfileComponent {
  tests: Array<Test> = new Array<Test>();

  constructor(private _tests: TestService,
              private _token: TokenService,
              private _router: Router) {
  }

  ngOnInit(): void {
    if (this._token.role == Role.student) {
      this._router.navigate(['tests']);
      return;
    }
    if (!this._token.isJwtTokenExists()) {
      this._router.navigate(['login']);
      return;
    }
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => console.log(response),
      next: (next: Array<Test>) => {
        this.tests = next.filter(t => t.username == this._token.username);
        console.log(this.tests);
      },
      complete: () => {
      }
    }
    this._tests.get().subscribe(observer);
  }
}
