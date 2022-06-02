import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Test} from "../../models/tests/test.model";
import {TokenService} from "../tokens/token.service";
import {Observable} from "rxjs";

@Injectable()
export class TestService {
  private readonly _url = "http://localhost:5222/api/tests";

  constructor(private _http: HttpClient,
              private _token: TokenService) {
  }

  get(id?: string): Observable<any> {
    const jwtToken = this._token.jwtToken;
    const options = {
      headers: new HttpHeaders({
        Authorization: `bearer ${jwtToken}`
      }),
    };

    const getString = id != undefined ? (this._url + '/' + id) : this._url;
    return this._http.get(getString, options);
  }

  create(test: Test): Observable<any> {
    const jwtToken = this._token.jwtToken;
    const options = {
      headers: new HttpHeaders({
        Authorization: `bearer ${jwtToken}`
      }),
      observe: 'response' as 'body'
    };
    return this._http.post(this._url, test, options);
  }
}
