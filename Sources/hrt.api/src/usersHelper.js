const bcrypt = require('bcryptjs');


class UsersHelper {

    generateSalt(length = 5) {
        let salt = "";
        for(i = 0; i < length; ++i) {
            salt += 65 + Math.random() * 26;
        }

        return salt;
    }

    getPasswordHash(password, salt) {       

        let salted = password + toString(password.length) + salt;

        const hash = bcrypt.hashSync(salted, 8);

        return hash;
    }
}

module.exports = { UsersHelper }