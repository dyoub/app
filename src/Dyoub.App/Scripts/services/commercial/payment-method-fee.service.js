// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service() { }

    Service.prototype.create = function () {
        return { minimumInstallment: 1 };
    };

    Service.prototype.compare = function (fee, otherFee) {
        return fee.minimumInstallment === otherFee.minimumInstallment;
    };

    Service.prototype.hasFixedValue = function (paymentFee) {
        return angular.isNumber(paymentFee.feeFixedValue);
    };

    Service.prototype.hasPercentage = function (paymentFee) {
        return angular.isNumber(paymentFee.feePercentage);
    };

    Service.prototype.hasPercentageOrFixedValue = function (paymentFee) {
        return this.hasPercentage(paymentFee) || this.hasFixedValue(paymentFee);
    };

    angular.module('dyoub.app').service('PaymentMethodFee', [
        Service
    ]);

})();
