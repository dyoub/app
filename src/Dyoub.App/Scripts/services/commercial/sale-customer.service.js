// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.find = function (saleOrderId) {
        return this.$http.post('/sale-orders/customer', { id: saleOrderId });
    };

    Service.prototype.save = function (saleCustomer) {
        return this.$http.post('/sale-orders/customer/update', {
            saleOrderId: saleCustomer.saleOrderId,
            customer: saleCustomer.customer === null ? null : {
                id: saleCustomer.customer.id,
                name: saleCustomer.customer.name,
                nationalId: saleCustomer.customer.nationalId,
                email: saleCustomer.customer.email,
                phoneNumber: saleCustomer.customer.phoneNumber,
                alternativePhoneNumber: saleCustomer.customer.alternativePhoneNumber
            }
        });
    };

    angular.module('dyoub.app').service('SaleCustomer', [
        '$http',
        Service
    ]);

})();
