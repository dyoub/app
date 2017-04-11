// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Product) {
        this.handleError = handleError;
        this.path = path;
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

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/products/details/:productId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Product
            .remove(controller.routeParams.productId)
            .then(function () {
                controller.path.redirectTo('/products');
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

    angular.module('dyoub.app').controller('ProductDetailsController', [
        'HandleError',
        'Path',
        'Product',
        Controller
    ]);

})();
