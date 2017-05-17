// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http, Customer) {
        this.$http = $http;
        this.Customer = Customer;
    }

    Service.prototype.find = function (rentContractId) {
        return this.$http.post('/rent-contracts/customer', { id: rentContractId });
    };

    Service.prototype.saveOrder = function (rentContractCustomer) {
        return this.$http.post('/rent-contracts/customer/update', {
            rentContractId: rentContractCustomer.rentContractId,
            customerId: rentContractCustomer.customerId
        });
    };

    Service.prototype.save = function (rentContractCustomer) {
        var service = this;

        if (rentContractCustomer.customer) {
            return this.Customer.save(rentContractCustomer.customer).then(function (response) {
                rentContractCustomer.customerId = response.data.id;
                return service.saveOrder(rentContractCustomer);
            });
        } else {
            return service.saveOrder(rentContractCustomer);
        }
    };

    angular.module('dyoub.app').service('RentContractCustomer', [
        '$http',
        'Customer',
        Service
    ]);

})();
