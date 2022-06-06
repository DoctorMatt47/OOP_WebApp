import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {TokenService} from "../tokens/token.service";
import {Answer} from "../../models/tests/answer.model";
import {Observable} from "rxjs";

@Injectable()
export class AnswerService {
  private readonly _url = "http://localhost:5222/api/answers";

  constructor(private _http: HttpClient,
              private _token: TokenService) {
  }

  get(testId: string, username: string) : Observable<Array<Answer>> {
    const options = {
      headers: new HttpHeaders({
        Authorization: `bearer ${this._token.jwtToken}`
      }),
    };
    return this._http.get<Array<Answer>>(this._url + `?testId=${testId}&username=${username}`, options);
  }

  create(answers: Array<Answer>) : Observable<object> {
    const options = {
      headers: new HttpHeaders({
        Authorization: `bearer ${this._token.jwtToken}`
      }),
      observe: 'response' as 'body'
    };
    return this._http.post(this._url, answers, options);
  }
}
