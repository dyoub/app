// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http, Customer) {
        this.$http = $http;
        this.Customer = Customer;
    }

    Service.prototype.find = function (saleOrderId) {
        return this.$http.post('/sale-orders/customer', { id: saleOrderId });
    };

    Service.prototype.saveOrder = function (saleCustomer) {
        return this.$http.post('/sale-orders/customer/update', {
            saleOrderId: saleCustomer.saleOrderId,
            customerId: saleCustomer.customerId
        });
    };

    Service.prototype.save = function (saleCustomer) {
        var service = this;

        if (saleCustomer.customer) {
            return this.Customer.save(saleCustomer.customer).then(function (response) {
                saleCustomer.customerId = response.data.id;
                return service.saveOrder(saleCustomer);
            });
        } else {
            return service.saveOrder(saleCustomer);
        }
    };

    angular.module('dyoub.app').service('SaleCustomer', [
        '$http',
        'Customer',
        Service
    ]);

})();
