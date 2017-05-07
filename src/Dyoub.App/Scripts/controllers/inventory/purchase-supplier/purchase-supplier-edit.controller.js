// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Supplier, handleError, path, PurchaseSupplier) {
        this.path = path;
        this.handleError = handleError;
        this.Supplier = Supplier;
        this.PurchaseSupplier = PurchaseSupplier;
    }

    Controller.prototype.clearSupplierData = function () {
        var controller = this;
        controller.purchaseOrder.supplier = null;
    };

    Controller.prototype.hasSupplierData = function () {
        var controller = this;

        if (!controller.purchaseOrder || !controller.purchaseOrder.supplier) {
            return false;
        }

        for (var property in controller.purchaseOrder.supplier) {
            if (controller.purchaseOrder.supplier[property]) {
                return true;
            }
        }

        return false;
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PurchaseSupplier
            .find(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.purchaseOrder = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.getSupplier = function () {
        var controller = this;

        if (!controller.hasSupplierData()) {
            return null;
        }

        return controller.purchaseOrder.supplier
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/purchase-orders/edit/:purchaseOrderId/supplier');

        controller.find();
    };

    Controller.prototype.newSupplierSearch = function () {
        var controller = this;
        controller.supplierSuggestionOpened = true;
        controller.searchingSuppliers = true;
        controller.supplierList = [];
    };

    Controller.prototype.noSuppliers = function () {
        var controller = this;
        return controller.supplierList &&
            controller.supplierList.isEmpty() &&
            !controller.searchingSuppliers;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.purchaseSupplierForm.$valid && !controller.saving) {
            controller.saving = true;

            controller.PurchaseSupplier.save({
                purchaseOrderId: controller.routeParams.purchaseOrderId,
                supplier: controller.getSupplier()
            })
            .then(function () {
                controller.path.redirectTo('/purchase-orders/edit/:purchaseOrderId/items',
                    controller.routeParams);
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.saving = false;
            });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.purchaseSupplierForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.searchSuppliers = function () {
        var controller = this;
        controller.searchingSuppliers = true;

        controller.Supplier.list({
            name: controller.purchaseOrder.supplier.name,
            index: 0
        })
        .then(function (response) {
            controller.supplierList = response.data;
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searchingSuppliers = false;
        });
    };

    Controller.prototype.selectSupplier = function (supplier) {
        var controller = this;
        controller.purchaseOrder.supplier = supplier;
        controller.supplierSuggestionOpened = false;
    };

    angular.module('dyoub.app').controller('PurchaseSupplierEditController', [
        'Supplier',
        'HandleError',
        'Path',
        'PurchaseSupplier',
        Controller
    ]);

})();
