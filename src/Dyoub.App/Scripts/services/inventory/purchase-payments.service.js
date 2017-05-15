// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/payments', { id: purchaseOrderId });
    };

    Service.prototype.save = function (purchasePayments) {
        console.info(purchasePayments);

        return this.$http.post('/purchase-orders/payments/update', {
            purchaseOrderId: purchasePayments.purchaseOrderId,
            shippingCost: purchasePayments.shippingCost,
            otherTaxes: purchasePayments.otherTaxes,
            discount: purchasePayments.discount,
            payments: purchasePayments.payments.select(function (payment) {
                return {
                    date: payment.date,
                    total: payment.total,
                    numberOfInstallments: payment.numberOfInstallments
                };
            })
        });
    };

    angular.module('dyoub.app').service('PurchasePayments', [
        '$http',
        Service
    ]);

})();
