// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, RentedProducts, RentedProductsReturn) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.RentedProducts = RentedProducts;
        this.RentedProductsReturn = RentedProductsReturn;
    }

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = this.path
            .map('/rent-contracts/edit/:rentContractId/return');

        controller.searchRentedProducts();
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.RentedProductsReturn
            .save(controller.rentContract)
            .then(function () {
                controller.path.redirectTo('/rent-contracts/details/:rentContractId/return',
                    controller.routeParams);
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.saving = false;
            });
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving ||
               controller.returnedProductsForm.$invalid ||
               !controller.rentContract ||
               !controller.rentContract.productList ||
                controller.rentContract.productList.isEmpty();
    };

    Controller.prototype.searchRentedProducts = function () {
        var controller = this;
        controller.searchingRentedProducts = true;

        controller.RentedProducts
            .list(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;

                controller.setInitialValues();
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searchingRentedProducts = false;
            });
    };

    Controller.prototype.searchRentedProductsComplete = function () {
        var controller = this;
        return !(controller.searchingRentedProducts || controller.error);
    };

    Controller.prototype.setInitialValues = function () {
        var controller = this;

        if (!controller.rentContract) return;

        controller.rentContract.dateOfReturn = controller.rentContract.dateOfReturn ||
            controller.rentContract.endDate;

        controller.rentContract.timeOfReturn = controller.rentContract.timeOfReturn ||
            controller.rentContract.endTime;

        for (var i = 0; i < controller.rentContract.productList.length; i++) {

            var rentedProduct = controller.rentContract.productList[i];
            rentedProduct.returnedQuantity = rentedProduct.quantity;
        }
    };

    angular.module('dyoub.app').controller('RentedProductReturnEditController', [
        'Dialog',
        'HandleError',
        'Path',
        'RentedProducts',
        'RentedProductsReturn',
        Controller
    ]);

})();
