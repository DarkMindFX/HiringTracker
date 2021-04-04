const bcrypt = require('bcryptjs');


class UsersHelper {

    generateSalt(length = 5) {
        let salt = bcrypt.genSaltSync(length);

        return salt;
    }

    getPasswordHash(password, salt) {       

        const hash = bcrypt.hashSync(password, salt);

        return hash;
    }
}

module.exports = { UsersHelper }