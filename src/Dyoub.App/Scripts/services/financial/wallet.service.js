// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return { active: true };
    };

    Service.prototype.find = function (walletId) {
        return this.$http.post('/wallets/find', { id: walletId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/wallets', filter);
    };

    Service.prototype.remove = function (walletId) {
        return this.$http.post('/wallets/delete', { id: walletId });
    };

    Service.prototype.save = function (wallet) {
        var action = wallet.id ? '/wallets/update' : '/wallets/create';

        return this.$http.post(action, {
            id: wallet.id,
            name: wallet.name,
            additionalInformation: wallet.additionalInformation,
            active: wallet.active
        });
    };

    angular.module('dyoub.app').service('Wallet', [
        '$http',
        Service
    ]);

})();
