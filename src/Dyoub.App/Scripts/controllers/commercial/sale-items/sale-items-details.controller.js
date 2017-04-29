// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, SaleItems) {
        this.path = path;
        this.handleError = handleError;
        this.SaleItems = SaleItems;
    }

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/sale-orders/details/:saleOrderId/items');

        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.SaleItems
            .list(controller.routeParams.saleOrderId)
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

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('SaleItemsDetailsController', [
        'HandleError',
        'Path',
        'SaleItems',
        Controller
    ]);

})();
