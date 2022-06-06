import {Injectable} from "@angular/core";
import {CookieService} from "ngx-cookie-service";
import {Role} from "../../models/users/role.enum";

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor(
    private _cookie: CookieService
  ) {
  }

  get username() : string {
    return this._cookie.get("OOP_WebApp_username");
  }

  set username(value: string) {
    this._cookie.delete("OOP_WebApp_username");
    this._cookie.set("OOP_WebApp_username", value, undefined, "/");
  }

  get role() : Role {
    return Number.parseInt(this._cookie.get("OOP_WebApp_role"));
  }

  set role(value: Role) {
    this._cookie.delete("OOP_WebApp_role");
    this._cookie.set("OOP_WebApp_role", value.toString(), undefined, "/");
  }

  get jwtToken(): string {
    return this._cookie.get("OOP_WebApp_jwtToken");
  }

  set jwtToken(value: string) {
    this._cookie.delete("OOP_WebApp_jwtToken", value);
    this._cookie.set("OOP_WebApp_jwtToken", value, undefined, "/");
  }

  isJwtTokenExists() {
    return this._cookie.check("OOP_WebApp_jwtToken");
  }

  deleteJwtToken() {
    this._cookie.delete("OOP_WebApp_jwtToken");
    this._cookie.delete("OOP_WebApp_role");
    this._cookie.delete("OOP_WebApp_username");
  }
}
