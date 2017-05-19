// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, SalePayments) {
        this.path = path;
        this.handleError = handleError;
        this.SalePayments = SalePayments;
    }

    Controller.prototype.getInstallmentOption = function (payment) {
        return {
            numberOfInstallments: payment.numberOfInstallments,
            installmentValue: (payment.valuePayable / payment.numberOfInstallments).round(2)
        };
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/sale-orders/details/:saleOrderId');

        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.SalePayments
            .list(controller.routeParams.saleOrderId)
            .then(function (response) {
                controller.saleOrder = response.data;
                controller.notFound = !controller.saleOrder;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.showConfirmDialog = function () {
        var controller = this;
        controller.confirming = false;
        controller.confirmDialogOpened = true;
    };

    angular.module('dyoub.app').controller('SalePaymentsDetailsController', [
        'HandleError',
        'Path',
        'SalePayments',
        Controller
    ]);

})();
