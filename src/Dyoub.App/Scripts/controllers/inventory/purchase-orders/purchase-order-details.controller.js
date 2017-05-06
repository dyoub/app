// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PurchaseOrder) {
        this.handleError = handleError;
        this.path = path;
        this.PurchaseOrder = PurchaseOrder;
    }

    Controller.prototype.confirm = function () {
        var controller = this;
        controller.confirming = true;

        controller.PurchaseOrder
            .confirm(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.purchaseOrder = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.confirming = false;
            })
            ['finally'](function () {
                controller.hideConfirmDialog();
            });
    };

    Controller.prototype.hideConfirmDialog = function () {
        var controller = this;
        controller.confirmDialogOpened = false;
    };

    Controller.prototype.hideRevertDialog = function () {
        var controller = this;
        controller.revertDialogOpened = false;
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PurchaseOrder
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

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/purchase-orders/details/:purchaseOrderId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.PurchaseOrder
            .remove(controller.routeParams.purchaseOrderId)
            .then(function () {
                controller.path.redirectTo('/purchase-orders');
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.hideRemoveDialog();
            });
    };

    Controller.prototype.revert = function () {
        var controller = this;
        controller.reverting = true;

        controller.PurchaseOrder
            .revert(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.purchaseOrder = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.reverting = false;
            })
            ['finally'](function () {
                controller.hideRevertDialog();
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

    Controller.prototype.showRevertDialog = function () {
        var controller = this;
        controller.reverting = false;
        controller.revertDialogOpened = true;
    };

    Controller.prototype.showRemoveDialog = function () {
        var controller = this;
        controller.removing = false;
        controller.removeDialogOpened = true;
    };

    angular.module('dyoub.app').controller('PurchaseOrderDetailsController', [
        'HandleError',
        'Path',
        'PurchaseOrder',
        Controller
    ]);

})();
