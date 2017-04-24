// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.changePassword = function (user) {
        return this.$http.post('/profile/change-password', {
            oldPassword: user.oldPassword,
            newPassword: user.newPassword
        });
    };

    Service.prototype.find = function () {
        return this.$http.post('/profile/find');
    };

    Service.prototype.save = function (user) {
        return this.$http.post('/profile/update', {
            name: user.name
        });
    };

    angular.module('dyoub.app').service('Profile', [
        '$http',
        Service
    ]);

})();
