// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (rentContractId) {
        return this.$http.post('/rent-contracts/payments', { id: rentContractId });
    };

    Service.prototype.save = function (rentPayments) {
        return this.$http.post('/rent-contracts/payments/update', {
            rentContractId: rentPayments.rentContractId,
            discount: rentPayments.discount,
            payments: rentPayments.payments.select(function (payment) {
                return {
                    date: payment.date,
                    total: payment.total,
                    paymentMethodId: payment.method.id,
                    numberOfInstallments: payment.installmentOption.numberOfInstallments
                };
            })
        });
    };

    angular.module('dyoub.app').service('RentPayments', [
        '$http',
        Service
    ]);

})();
