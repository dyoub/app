// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, ProductStock) {
        this.handleError = handleError;
        this.ProductStock = ProductStock;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {};
    };

    Controller.prototype.init = function () {
        var controller = this;
        controller.clearFilter();
        controller.newSearch();
    };

    Controller.prototype.newSearch = function () {
        var controller = this;
        controller.filter.index = 0;
        controller.productStockList = [];
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.ProductStock
            .list(controller.filter)
            .then(function (response) {
                controller.productStockList.pushRange(response.data);
                controller.filter.index = controller.productStockList.length
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    angular.module('dyoub.app').controller('ProductStockListController', [
        'HandleError',
        'ProductStock',
        'Store',
        Controller
    ]);

})();
