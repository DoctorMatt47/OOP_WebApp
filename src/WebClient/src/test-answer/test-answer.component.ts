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
  selector: 'test-answer',
  templateUrl: './test-answer.component.html',
  styleUrls: [],
  providers: [TestService, AnswerService, HttpClient]
})
export class TestAnswerComponent implements OnInit {
  test: Test = new Test();
  answers: Array<Array<boolean>> = new Array<Array<boolean>>();
  username: string = this._route.snapshot.params['username'];

  constructor(private _route: ActivatedRoute,
              private _token: TokenService,
              private _tests: TestService,
              private _answers: AnswerService,
              private _router: Router) {
  }

  answer(i: number, j: number) {
    return this.answers[i][j];
  }

  ngOnInit(): void {
    this.test.id = this._route.snapshot.params['testId'];
    if (this._token.role == Role.student) {
      this._router.navigate(['tests']);
      return;
    }
    if (!this._token.isJwtTokenExists()) {
      this._router.navigate(['login']);
      return;
    }
    this._tests.get(this.test.id!).subscribe((test: Test) => {
      this.test = test
      this._answers.get(this.test.id!, this.username).subscribe(this.getAnswersHandler(this.test, this.answers));
    });
  }

  getAnswersHandler(test: Test, thisAnswers: Array<Array<boolean>>) {
    return (answers: Array<Answer>) => {
      for (let i = 0; i < test.questions.length; i++) {
        const question = test.questions[i];
        thisAnswers.push([])
        for (let j = 0; j < question.options.length; j++) {
          const option = question.options[j];
          const answer = answers.find(a => a.optionId == option.id);
          thisAnswers[i].push(answer != undefined);
        }
      }
    }
  }
}
