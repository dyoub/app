// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, PricingTable, RentedProducts) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.PricingTable = PricingTable;
        this.RentedProducts = RentedProducts;
    }

    Controller.prototype.addProduct = function (product) {
        var controller = this;

        var alreadyAdded = controller.rentContract.productList.where(function (selectedProduct) {
            return selectedProduct.id === product.id;
        })
        .any();

        if (alreadyAdded) {
            controller.dialog.error('Product already added.');
        } else {
            product.quantity = 1;
            controller.calculateProductTotalPayable(product);
            controller.rentContract.productList.push(product);
        }
    };

    Controller.prototype.addFirstProduct = function () {
        var controller = this;

        if (controller.productsForRent.any() && controller.productSuggestionOpened) {
            var firstProduct = controller.productsForRent.first();
            controller.addProduct(firstProduct);
        }
    };

    Controller.prototype.calculateProductTotalPayable = function (product) {
        var controller = this;

        if (!controller.rentContract) return;

        var quantity = product.quantity || 0,
            totalDays = controller.rentContract.totalDays,
            unitPrice = product.unitPrice || 0,
            total = quantity * unitPrice * totalDays
            discount = total * (product.discount || 0) / 100;

        return product.totalPayable = (total - discount).round(2);
    };

    Controller.prototype.calculateTotalPayable = function () {
        var controller = this;

        if (!controller.rentContract) return;

        var totalPayable = 0;

        angular.forEach(controller.rentContract.productList, function (product) {
            totalPayable += controller.calculateProductTotalPayable(product);
        });

        return totalPayable < 0 ? 0 : totalPayable;
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.productToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = this.path
            .map('/rent-contracts/edit/:rentContractId/products');

        controller.searchSelectedProducts();
    };

    Controller.prototype.newProductForRentSearch = function () {
        var controller = this;
        controller.productSuggestionOpened = true;
        controller.searchingProductsForRent = true;
        controller.productsForRent = [];
    };

    Controller.prototype.noProductsForRent = function () {
        var controller = this;
        return controller.productsForRent &&
            controller.productsForRent.isEmpty() &&
            !controller.searchingProductsForRent;
    };

    Controller.prototype.noProductsSelected = function () {
        var controller = this;
        return controller.rentContract &&
            controller.rentContract.productList &&
            controller.rentContract.productList.isEmpty() &&
            !controller.searchingSelectedProducts;
    };

    Controller.prototype.removeProduct = function () {
        var controller = this,
            productIndex = controller.rentContract.productList.indexOf(controller.productToRemove);

        controller.rentContract.productList.splice(productIndex, 1);
        controller.productToRemove = null;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.RentedProducts.save({
            rentContractId: controller.routeParams.rentContractId,
            products: controller.rentContract.productList
        })
        .then(function () {
            controller.path.redirectTo('/rent-contracts/edit/:rentContractId/payments',
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
               controller.rentedProductsForm.$invalid ||
               !controller.rentContract ||
               !controller.rentContract.productList ||
                controller.rentContract.productList.isEmpty();
    };

    Controller.prototype.searchProductsForRent = function () {
        var controller = this;
        controller.searchingProductsForRent = true;

        controller.PricingTable.listProductsForRent({
            storeId: controller.rentContract.storeId,
            nameOrCode: controller.productSearch.nameOrCode
        })
        .then(function (response) {
            controller.productsForRent = response.data;
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searchingProductsForRent = false;
        });
    };

    Controller.prototype.searchSelectedProducts = function () {
        var controller = this;
        controller.searchingSelectedProducts = true;

        controller.RentedProducts
            .list(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searchingSelectedProducts = false;
            });
    };

    Controller.prototype.searchSelectedProductsComplete = function () {
        var controller = this;
        return !(controller.searchingSelectedProducts || controller.error);
    };

    Controller.prototype.showRemoveProductDialog = function (product) {
        var controller = this;
        controller.productToRemove = product;
    };

    angular.module('dyoub.app').controller('RentedProductsEditController', [
        'Dialog',
        'HandleError',
        'Path',
        'PricingTable',
        'RentedProducts',
        Controller
    ]);

})();
