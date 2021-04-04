
class LoginRequest {

    set Login(val) { this._login = val; }
    get Login()    { return this._login; }

    set Password(val)   { this._pwd = val; }
    get Password()      { return this._pwd; }

}

class LoginResponse {

    set Token(val)  { this._token = val; }
    get Token()     { return this._token; }

    set Expires(val){ this._expires = val; }
    get Expires()   { return this._expires; }
}

module.exports = {
    LoginRequest,
    LoginResponse
}