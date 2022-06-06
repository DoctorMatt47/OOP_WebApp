import {Component} from '@angular/core';
import {Test} from "../models/tests/test.model";
import {TestService} from "../services/api/test.service";
import {Observer} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {TokenService} from "../services/tokens/token.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Role} from "../models/users/role.enum";
import {User} from "../models/users/user.model";
import {UserService} from "../services/api/user.service";

@Component({
  selector: 'test-answers',
  templateUrl: './test-answers.component.html',
  styleUrls: [],
  providers: [UserService, HttpClient]
})
export class TestAnswersComponent {
  users: Array<User> = new Array<User>();
  testId: string = this._route.snapshot.params['id'];

  constructor(private _users: UserService,
              private _token: TokenService,
              private _route: ActivatedRoute,
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
      next: (next: Array<User>) => {
        this.users = next;
        console.log(this.users);
      },
      complete: () => {
      }
    }
    this._users.get(this.testId).subscribe(observer);
  }
}
