// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, RentContractCustomer) {
        this.path = path;
        this.handleError = handleError;
        this.RentContractCustomer = RentContractCustomer;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.RentContractCustomer
            .find(controller.routeParams.rentContractId)
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

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/rent-contracts/details/:rentContractId/customer');

        controller.find();
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

    angular.module('dyoub.app').controller('RentContractCustomerDetailsController', [
        'HandleError',
        'Path',
        'RentContractCustomer',
        Controller
    ]);

})();
