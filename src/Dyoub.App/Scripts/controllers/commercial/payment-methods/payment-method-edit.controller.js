// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(PaymentMethod, handleError, path) {
        this.PaymentMethod = PaymentMethod;
        this.path = path;
        this.handleError = handleError;
    }

    Controller.prototype.addPaymentFee = function () {
        var controller = this;
        controller.paymentMethod.paymentFees
            .push(controller.PaymentMethod.Fee.create());
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PaymentMethod
            .find(controller.routeParams.paymentMethodId)
            .then(function (response) {
                controller.paymentMethod = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.goBack = function () {
        var controller = this;

        if (controller.routeParams.action === 'edit') {
            controller.path.redirectTo('/payment-methods/details/:paymentMethodId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/payment-methods');
        }
    };

    Controller.prototype.hasDuplicatePaymentFee = function () {
        var controller = this;

        if (!controller.paymentMethod) {
            return false;
        }

        return controller.paymentMethod.paymentFees
            .hasDuplicate(controller.PaymentMethod.Fee.compare);
    };

    Controller.prototype.hasPercentage = function (paymentFee) {
        return this.PaymentMethod.Fee.hasPercentage(paymentFee);
    };

    Controller.prototype.hasFixedValue = function (paymentFee) {
        return this.PaymentMethod.Fee.hasFixedValue(paymentFee);
    };

    Controller.prototype.hasPercentageOrFixedValue = function (paymentFee) {
        return this.PaymentMethod.Fee.hasPercentageOrFixedValue(paymentFee);
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/payment-methods/:action/:paymentMethodId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit payment method';
            controller.find();
        } else {
            controller.pageHeader = 'New payment method';
            controller.paymentMethod = controller.PaymentMethod.create();
        }

        controller.installmentConditions = controller.PaymentMethod.Installment.create(24);
        controller.installmentLimitOptions = controller.PaymentMethod.Installment.create(24);
        controller.receivedDateOptions = controller.PaymentMethod.receivedDateOptions(31);
    };

    Controller.prototype.removePaymentFee = function (paymentFee) {
        var controller = this;
        controller.paymentMethod.paymentFees.remove(paymentFee);
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.PaymentMethod
                .save(controller.paymentMethod)
                .then(function () {
                    controller.goBack();
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.saving = false;
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;

        return controller.saving ||
            controller.paymentMethodForm.$invalid ||
            controller.hasDuplicatePaymentFee();
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('PaymentMethodEditController', [
        'PaymentMethod',
        'HandleError',
        'Path',
        Controller
    ]);

})();
