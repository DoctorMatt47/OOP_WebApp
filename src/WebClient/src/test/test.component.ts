import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Test} from "../models/tests/test.model";
import {Observer} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {TestService} from "../services/api/test.service";

@Component({
  selector: 'test',
  templateUrl: './test.component.html',
  styleUrls: [],
  providers: [TestService, HttpClient]
})
export class TestComponent implements OnInit {
  test: Test = new Test();

  constructor(private _route: ActivatedRoute,
              private _tests: TestService,
              private _router: Router) {
  }

  ngOnInit(): void {
    this.test.id = this._route.snapshot.params['id'];
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => {
        console.log(response);
      },
      next: (next: Test) => {
        this.test = next;
        console.log(this.test);
      },
      complete: () => {
      }
    }
    this._tests.get(this.test.id!).subscribe(observer);
  }
}
