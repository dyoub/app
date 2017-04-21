// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http, PaymentMethodFee, Installment) {
        this.$http = $http;
        this.Fee = PaymentMethodFee;
        this.Installment = Installment;
    }

    Service.prototype.create = function () {
        return {
            active: true,
            earlyReceipt: false,
            installmentLimit: 1,
            paymentFees: []
        };
    };

    Service.prototype.find = function (id) {
        return this.$http.post('/payment-methods/find', { id: id });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/payment-methods', filter);
    };

    Service.prototype.receivedDateOptions = function (max) {
        var options = [];

        for (var daysToReceive = 0; daysToReceive <= max; daysToReceive++) {
            options.push(daysToReceive);
        }

        return options;
    };

    Service.prototype.remove = function (id) {
        return this.$http.post('/payment-methods/delete', { id: id });
    };

    Service.prototype.save = function (paymentMethod) {
        var action = paymentMethod.id ? '/payment-methods/update' : '/payment-methods/create';

        return this.$http.post(action, {
            id: paymentMethod.id,
            name: paymentMethod.name,
            installmentLimit: paymentMethod.installmentLimit,
            daysToReceive: paymentMethod.daysToReceive,
            earlyReceipt: paymentMethod.earlyReceipt,
            paymentFees: paymentMethod.paymentFees,
            active: paymentMethod.active
        });
    };

    angular.module('dyoub.app').service('PaymentMethod', [
        '$http',
        'PaymentMethodFee',
        'Installment',
        Service
    ]);

})();
