// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {};
    };

    Service.prototype.find = function (supplierId) {
        return this.$http.post('/suppliers/find', { id: supplierId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/suppliers', filter);
    };

    Service.prototype.remove = function (supplierId) {
        return this.$http.post('/suppliers/delete', { id: supplierId });
    };

    Service.prototype.save = function (supplier) {
        var action = supplier.id ? '/suppliers/update' : '/suppliers/create';

        return this.$http.post(action, {
            id: supplier.id,
            name: supplier.name,
            nationalId: supplier.nationalId,
            email: supplier.email,
            phoneNumber: supplier.phoneNumber,
            alternativePhoneNumber: supplier.alternativePhoneNumber,
            homepage: supplier.homepage,
            facebook: supplier.facebook,
            instagram: supplier.instagram,
            twitter: supplier.twitter,
            googlePlus: supplier.googlePlus
        });
    };

    angular.module('dyoub.app').service('Supplier', [
        '$http',
        Service
    ]);

})();
