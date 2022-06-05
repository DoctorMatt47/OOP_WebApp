import {Component} from '@angular/core';
import {Test} from "../models/tests/test.model";
import {TestService} from "../services/api/test.service";
import {Observer} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {TokenService} from "../services/tokens/token.service";
import {Router} from "@angular/router";

@Component({
  selector: 'tests',
  templateUrl: './tests.component.html',
  styleUrls: [],
  providers: [TokenService, TestService, HttpClient]
})
export class TestsComponent {
  tests: Array<Test> = new Array<Test>();

  constructor(private _tests: TestService,
              private _token: TokenService,
              private _router: Router) {
  }

  ngOnInit(): void {
    if (!this._token.isJwtTokenExists()) {
      this._router.navigate(['login']);
      return;
    }
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => {
        console.log(response);
      },
      next: (next: Array<Test>) => {
        this.tests = next;
        console.log(this.tests);
      },
      complete: () => {
      }
    }
    this._tests.get().subscribe(observer);
  }
}
