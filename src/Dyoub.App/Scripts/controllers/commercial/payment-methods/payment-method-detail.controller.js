// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(PaymentMethod, handleError, path) {
        this.PaymentMethod = PaymentMethod;
        this.handleError = handleError;
        this.path = path;
    }

    Controller.prototype.detailsFrom = function (paymentMethod) {
        if (paymentMethod) {
            for (var i = 0; i < paymentMethod.paymentFees.length; i++) {
                var paymentFee = paymentMethod.paymentFees[i],
                    nextPaymentFee = paymentMethod.paymentFees[i + 1];

                paymentFee.maximumInstallment = !!nextPaymentFee ?
                    nextPaymentFee.minimumInstallment - 1 :
                    paymentMethod.installmentLimit;
            }
        }

        return paymentMethod;
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PaymentMethod
            .find(controller.routeParams.paymentMethodId)
            .then(function (response) {
                controller.paymentMethod = controller.detailsFrom(response.data);
                controller.notFound = !controller.paymentMethod;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/payment-methods/details/:paymentMethodId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.PaymentMethod
            .remove(controller.routeParams.paymentMethodId)
            .then(function () {
                controller.path.redirectTo('/payment-methods');
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.hideRemoveDialog();
            });
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.showRemoveDialog = function () {
        var controller = this;
        controller.removing = false;
        controller.removeDialogOpened = true;
    };

    angular.module('dyoub.app').controller('PaymentMethodDetailsController', [
        'PaymentMethod',
        'HandleError',
        'Path',
        Controller
    ]);

})();
