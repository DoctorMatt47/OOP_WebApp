import {Injectable} from "@angular/core";
import {CookieService} from "ngx-cookie-service";
import {Role} from "../../models/users/role.enum";

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  public role?: Role;

  constructor(
    private _cookie: CookieService
  ) {
  }

  get jwtToken(): string {
    return this._cookie.get("OOP_WebApp_jwtToken");
  }

  set jwtToken(value: string) {
    this._cookie.set("OOP_WebApp_jwtToken", value);
  }

  isJwtTokenExists() {
    return this._cookie.check("OOP_WebApp_jwtToken");
  }

  deleteJwtToken() {
    this._cookie.delete("OOP_WebApp_jwtToken");
  }
}
