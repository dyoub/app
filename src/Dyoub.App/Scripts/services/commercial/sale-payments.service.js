// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (saleOrderId) {
        return this.$http.post('/sale-orders/payments', { id: saleOrderId });
    };

    Service.prototype.save = function (salePayments) {
        return this.$http.post('/sale-orders/payments/update', {
            saleOrderId: salePayments.saleOrderId,
            discount: salePayments.discount,
            payments: salePayments.payments.select(function (payment) {
                return {
                    date: payment.date,
                    total: payment.total,
                    paymentMethodId: payment.method.id,
                    numberOfInstallments: payment.installmentOption.numberOfInstallments
                };
            })
        });
    };

    angular.module('dyoub.app').service('SalePayments', [
        '$http',
        Service
    ]);

})();
