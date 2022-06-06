import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {User} from "../../models/users/user.model";
import {Observable} from "rxjs";

@Injectable()
export class UserService {
  private readonly _url = "http://localhost:5222/api/Users";

  constructor(private _http: HttpClient) {
  }

  authenticate(username: string, password: string) {
    let body = {
      username: username,
      password: password
    }
    return this._http.post(this._url + '/Authenticate', body);
  }

  create(acc: User) {
    let body = {
      username: acc.username,
      password: acc.password,
    }
    return this._http.post(this._url, body);
  }

  get(testId: string) : Observable<Object> {
    return this._http.get(this._url + `?testId=${testId}`);
  }
}
