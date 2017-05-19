// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, RentPayments) {
        this.path = path;
        this.handleError = handleError;
        this.RentPayments = RentPayments;
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
            .map('/rent-contracts/details/:rentContractId');

        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.RentPayments
            .list(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;
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

    angular.module('dyoub.app').controller('RentPaymentsDetailsController', [
        'HandleError',
        'Path',
        'RentPayments',
        Controller
    ]);

})();
