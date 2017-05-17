// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, SaleCustomer) {
        this.path = path;
        this.handleError = handleError;
        this.SaleCustomer = SaleCustomer;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.SaleCustomer
            .find(controller.routeParams.saleOrderId)
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

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/sale-orders/details/:saleOrderId/customer');

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

    angular.module('dyoub.app').controller('SaleCustomerDetailsController', [
        'HandleError',
        'Path',
        'SaleCustomer',
        Controller
    ]);

})();
