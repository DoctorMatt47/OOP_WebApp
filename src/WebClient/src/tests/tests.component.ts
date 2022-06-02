import {Component} from '@angular/core';
import {Test} from "../models/tests/test.model";
import {TestService} from "../services/api/test.service";
import {Observer} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {TokenService} from "../services/tokens/token.service";

@Component({
  selector: 'search',
  templateUrl: './tests.component.html',
  styleUrls: [],
  providers: [TokenService, TestService, HttpClient]
})
export class TestsComponent {
  tests: Array<Test> = new Array<Test>();

  constructor(private _tests: TestService) {
  }

  ngOnInit(): void {
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
