
class UserEntity {

    get UserID() { return this._userId; }
    set UserID(val) { this._userId = val; }

    get Login() { return this._login; }
    set Login(val) { this._login = val; }

    get FirstName() { return this._fname; }
    set FirstName(val) { this._fname = val; }

    get LastName() { return this._lname; }
    set LastName(val) { this._lname = val; }

    get Email() { return this._email; }
    set Email(val) { this._email = val; }

    get Description() { return this._desc; }
    set Description(val) { this._desc = val; }

    get PwdHash() { return this._pwdHash; }
    set PwdHash(val) { this._pwdHash = val; }

    get Salt() { return this._salt; }
    set Salt(val) { this._salt = val; }

}

module.exports = UserEntity;