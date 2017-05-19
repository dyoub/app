// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PurchasedProducts) {
        this.path = path;
        this.handleError = handleError;
        this.PurchasedProducts = PurchasedProducts;
    }

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/purchase-orders/details/:purchaseOrderId/items');

        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.PurchasedProducts
            .list(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.purchaseOrder = response.data;
                controller.notFound = !controller.purchaseOrder;
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

    angular.module('dyoub.app').controller('PurchasedProductsDetailsController', [
        'HandleError',
        'Path',
        'PurchasedProducts',
        Controller
    ]);

})();
