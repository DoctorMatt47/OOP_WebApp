import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient, HttpErrorResponse, HttpResponse} from "@angular/common/http";
import {Observer} from "rxjs";
import {Question} from "../models/tests/question.model";
import {Test} from "../models/tests/test.model";
import {TokenService} from "../services/tokens/token.service";
import {TestService} from "../services/api/test.service";
import {Option} from "../models/tests/option.model";

@Component({
  selector: 'creator',
  templateUrl: './creator.component.html',
  styleUrls: [],
  providers: [TestService, HttpClient]
})
export class CreatorComponent implements OnInit {
  questions: Array<Question> = new Array<Question>();
  test: Test = new Test();
  error: string = '';

  constructor(private _tests: TestService,
              private _token: TokenService,
              private _router: Router) {
    this.test.questions = new Array<Question>();
  }

  ngOnInit(): void {
    if (!this._token.isJwtTokenExists()) {
      const redirectState = {state: {redirect: this._router.url}};
      this._router.navigate(['/login'], redirectState);
    }
  }

  addQuestion(): void {
    if (this.questions.length > 50) {
      return;
    }
    const newQuestion = new Question();
    newQuestion.string = '';

    newQuestion.options = new Array<Option>();

    this.test.questions.push(newQuestion);
  }

  addChoice(question: Question): void {
    if (question.options.length > 50) {
      return;
    }
    const newChoice = new Option();
    newChoice.string = '';
    question.options.push(newChoice);
  }

  removeChoice(question: Question, choiceIndex: number) {
    question.options.splice(choiceIndex, 1);
  }

  removeQuestion(questionIndex: number): void {
    this.test.questions.splice(questionIndex, 1);
  }

  createTest() {
    let observer: Observer<any> = {
      error: (response: HttpErrorResponse) => {
        if (response.status == 403)
          this.error = `You don't have permissions to create test`;
        console.log(response);
      },
      next: (next: HttpResponse<any>) => {
        this.test.id = next.body.id;
        console.log(next.url);
        console.log(next.body);
      },
      complete: () => {
        const testPreviewUrl = `/test/${this.test.id}`;
        this._router.navigate([testPreviewUrl]);
      }
    }
    console.log(this.test);
    this._tests.create(this.test).subscribe(observer)
  }
}
