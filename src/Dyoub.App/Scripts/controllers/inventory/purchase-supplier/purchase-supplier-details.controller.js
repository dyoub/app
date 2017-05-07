// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PurchaseSupplier) {
        this.path = path;
        this.handleError = handleError;
        this.PurchaseSupplier = PurchaseSupplier;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PurchaseSupplier
            .find(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.purchaseOrder = response.data;
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
            .map('/purchase-orders/details/:purchaseOrderId/supplier');

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

    angular.module('dyoub.app').controller('PurchaseSupplierDetailsController', [
        'HandleError',
        'Path',
        'PurchaseSupplier',
        Controller
    ]);

})();
