// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, ProductStock) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.ProductStock = ProductStock;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {
            storeId: controller.routeParams.storeId
        };
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.finding = true;

        controller.ProductStock
            .find(controller.routeParams.storeId)
            .then(function (response) {
                controller.store = response.data;
                controller.notFound = !controller.store;

                if (controller.store) {
                    controller.newSearchOfProducts();
                }
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.storeError = true;
            })
            ['finally'](function () {
                controller.finding = false;
            });
    };

    Controller.prototype.findComplete = function () {
        var controller = this;
        return !(controller.finding || controller.storeError);
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/product-stock/details/:storeId');

        controller.clearFilter();
        controller.find();
    };

    Controller.prototype.newSearchOfProducts = function () {
        var controller = this;
        controller.filter.index = 0;
        controller.productList = [];
        controller.searchProducts();
    };

    Controller.prototype.searchProducts = function () {
        var controller = this;
        controller.searchingProducts = true;

        controller.ProductStock
            .listProducts(controller.filter)
            .then(function (response) {
                controller.productList.pushRange(response.data);
                controller.filter.index = controller.productList.length;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.productsError = true;
            })
            ['finally'](function () {
                controller.searchingProducts = false;
            });
    };

    Controller.prototype.searchProductsComplete = function () {
        var controller = this;
        return !(controller.searchingProducts || controller.productsError);
    };

    angular.module('dyoub.app').controller('ProductStockDetailsController', [
        'Dialog',
        'HandleError',
        'Path',
        'ProductStock',
        Controller
    ]);

})();
