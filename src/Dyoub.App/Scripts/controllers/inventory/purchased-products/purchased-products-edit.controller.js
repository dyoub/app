// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, Product, PurchasedProducts) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.Product = Product;
        this.PurchasedProducts = PurchasedProducts;
    }

    Controller.prototype.addProduct = function (product) {
        var controller = this;

        var alreadyAdded = controller.purchaseOrder.productList.where(function (selectedProduct) {
            return selectedProduct.id === product.id;
        })
        .any();

        if (alreadyAdded) {
            controller.dialog.error('Product already added.');
        } else {
            product.quantity = 1;
            controller.calculateProductTotalCost(product);
            controller.purchaseOrder.productList.push(product);
        }
    };

    Controller.prototype.addFirstProduct = function () {
        var controller = this;

        if (controller.productsForPurchase.any() && controller.productSuggestionOpened) {
            var firstProduct = controller.productsForPurchase.first();
            controller.addProduct(firstProduct);
        }
    };

    Controller.prototype.calculateProductTotalCost = function (product) {
        var quantity = product.quantity || 0,
            unitCost = product.unitCost || 0,
            discount = product.discount || 0;

        return product.totalCost = (quantity * unitCost - discount).round(2);
    };

    Controller.prototype.calculateTotalCost = function () {
        var controller = this;

        if (!controller.purchaseOrder) return;

        var totalCost = 0;

        angular.forEach(controller.purchaseOrder.productList, function (product) {
            totalCost += controller.calculateProductTotalCost(product);
        });

        return totalCost < 0 ? 0 : totalCost;
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.productToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = this.path
            .map('/purchase-orders/edit/:purchaseOrderId/products');

        controller.searchSelectedProducts();
    };

    Controller.prototype.newProductForPurchaseSearch = function () {
        var controller = this;
        controller.productSuggestionOpened = true;
        controller.searchingProductsForPurchase = true;
        controller.productsForPurchase = [];
    };

    Controller.prototype.noProductsForPurchase = function () {
        var controller = this;
        return controller.productsForPurchase &&
            controller.productsForPurchase.isEmpty() &&
            !controller.searchingProductsForPurchase;
    };

    Controller.prototype.noProductsSelected = function () {
        var controller = this;
        return controller.purchaseOrder &&
            controller.purchaseOrder.productList &&
            controller.purchaseOrder.productList.isEmpty() &&
            !controller.searchingSelectedProducts;
    };

    Controller.prototype.removeProduct = function () {
        var controller = this,
            productIndex = controller.purchaseOrder.productList.indexOf(controller.productToRemove);

        controller.purchaseOrder.productList.splice(productIndex, 1);
        controller.productToRemove = null;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.PurchasedProducts.save({
            purchaseOrderId: controller.routeParams.purchaseOrderId,
            products: controller.purchaseOrder.productList
        })
        .then(function () {
            controller.path.redirectTo('/purchase-orders/edit/:purchaseOrderId/payments',
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
               controller.purchaseOrderProductsForm.$invalid ||
               !controller.purchaseOrder
        !controller.purchaseOrder.productList ||
         controller.purchaseOrder.productList.isEmpty();
    };

    Controller.prototype.searchProductsForPurchase = function () {
        var controller = this;
        controller.searchingProductsForPurchase = true;

        controller.Product.list({
            nameOrCode: controller.productSearch.nameOrCode,
            stockMovement: true,
            limit: 3
        })
        .then(function (response) {
            controller.productsForPurchase = response.data;
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searchingProductsForPurchase = false;
        });
    };

    Controller.prototype.searchSelectedProducts = function () {
        var controller = this;
        controller.searchingSelectedProducts = true;

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

    angular.module('dyoub.app').controller('PurchasedProductsEditController', [
        'Dialog',
        'HandleError',
        'Path',
        'Product',
        'PurchasedProducts',
        Controller
    ]);

})();
