// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, Product) {
        this.handleError = handleError;
        this.Product = Product;
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
        controller.productList = [];
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.Product
            .list(controller.filter)
            .then(function (response) {
                controller.productList.pushRange(response.data);
                controller.filter.index = controller.productList.length
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    angular.module('dyoub.app').controller('ProductListController', [
        'HandleError',
        'Product',
        Controller
    ]);

})();
