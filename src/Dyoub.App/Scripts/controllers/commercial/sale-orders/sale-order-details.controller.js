// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, SaleOrder) {
        this.handleError = handleError;
        this.path = path;
        this.SaleOrder = SaleOrder;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.SaleOrder
            .find(controller.routeParams.saleOrderId)
            .then(function (response) {
                controller.saleOrder = response.data;
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
            .map('/sale-orders/details/:saleOrderId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.SaleOrder
            .remove(controller.routeParams.saleOrderId)
            .then(function () {
                controller.path.redirectTo('/sale-orders');
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

    angular.module('dyoub.app').controller('SaleOrderDetailsController', [
        'HandleError',
        'Path',
        'SaleOrder',
        Controller
    ]);

})();
