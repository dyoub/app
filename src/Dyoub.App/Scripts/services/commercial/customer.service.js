// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {};
    };

    Service.prototype.find = function (customerId) {
        return this.$http.post('/customers/find', { id: customerId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/customers', filter);
    };

    Service.prototype.remove = function (customerId) {
        return this.$http.post('/customers/delete', { id: customerId });
    };

    Service.prototype.save = function (customer) {
        var action = customer.id ? '/customers/update' : '/customers/create';

        return this.$http.post(action, {
            id: customer.id,
            name: customer.name,
            nationalId: customer.nationalId,
            email: customer.email,
            phoneNumber: customer.phoneNumber,
            alternativePhoneNumber: customer.alternativePhoneNumber
        });
    };

    angular.module('dyoub.app').service('Customer', [
        '$http',
        Service
    ]);

})();
