// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Product) {
        this.path = path;
        this.handleError = handleError;
        this.Product = Product;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.Product
            .find(controller.routeParams.productId)
            .then(function (response) {
                controller.product = response.data;
                controller.notFound = !controller.product;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.goBack = function () {
        var controller = this;

        if (controller.routeParams.action === 'edit') {
            controller.path.redirectTo('/products/details/:productId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/products');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/products/:action/:productId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit product';
            controller.find();
        } else {
            controller.pageHeader = 'New product';
            controller.product = controller.Product.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Product
                .save(controller.product)
                .then(function () {
                    controller.goBack();
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.saving = false;
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.productForm.$invalid
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('ProductEditController', [
        'HandleError',
        'Path',
        'Product',
        Controller
    ]);

})();
