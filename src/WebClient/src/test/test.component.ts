import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Test} from "../models/tests/test.model";
import {Observer} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {TestService} from "../services/api/test.service";
import {Role} from "../models/users/role.enum";
import {TokenService} from "../services/tokens/token.service";
import {Answer} from "../models/tests/answer.model";
import {AnswerService} from "../services/api/answer.service";

@Component({
  selector: 'test',
  templateUrl: './test.component.html',
  styleUrls: [],
  providers: [TestService, AnswerService, HttpClient]
})
export class TestComponent implements OnInit {
  test: Test = new Test();
  username: string = this._token.username;
  answers: Array<Answer> = new Array<Answer>();

  constructor(private _route: ActivatedRoute,
              private _token: TokenService,
              private _tests: TestService,
              private _answers: AnswerService,
              private _router: Router) {
  }

  public checkedValue(questionId: string, optionId: string): boolean {
    return this.answers.find(a => a.questionId == questionId && a.optionId == optionId) != undefined;
  }

  ngOnInit(): void {
    this.test.id = this._route.snapshot.params['id'];
    if (this._token.role == Role.tutor) {
      this._router.navigate(['profile']);
      return;
    }
    if (!this._token.isJwtTokenExists()) {
      this._router.navigate(['login']);
      return;
    }
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => console.log(response),
      next: (next: Test) => {
        this.test = next;
        console.log(this.test);
      },
      complete: () => {
      }
    }
    this._tests.get(this.test.id!).subscribe(observer);
  }

  submit(): void {
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => console.log(response),
      next: (next: any) => console.log(next),
      complete: () => {
        this._router.navigate(['/tests']);
      }
    }
    this._answers.create(this.answers).subscribe(observer);
    console.log(this.answers);
  }

  changeOptionCheckbox($event: any, questionId: string, optionId: string): void {
    if (!$event.target.checked) {
      const index = this.answers.indexOf(this.answers.find(a => a.optionId == optionId)!);
      this.answers.splice(index, 1);
      return;
    }
    const answer = new Answer();
    answer.username = this.username;
    answer.testId = this.test.id;
    answer.questionId = questionId;
    answer.optionId = optionId;
    this.answers.push(answer)
  }
}
