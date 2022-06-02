import {Injectable} from "@angular/core";
import {CookieService} from "ngx-cookie-service";

@Injectable()
export class TokenService {
    constructor(
        private _cookie: CookieService
    ) {
    }

    isJwtTokenExists() {
        return this._cookie.check("OOP_WebApp_jwtToken");
    }

    get jwtToken(): string {
        return this._cookie.get("OOP_WebApp_jwtToken");
    }

    set jwtToken(value: string) {
        this._cookie.set("OOP_WebApp_jwtToken", value);
    }
}
