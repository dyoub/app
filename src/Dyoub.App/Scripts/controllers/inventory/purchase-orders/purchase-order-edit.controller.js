// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PurchaseOrder, Store, Wallet) {
        this.path = path;
        this.handleError = handleError;
        this.PurchaseOrder = PurchaseOrder;
        this.Store = Store;
        this.Wallet = Wallet;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PurchaseOrder
            .find(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.purchaseOrder = response.data;
                controller.notFound = !controller.purchaseOrder;
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
            controller.path.redirectTo('/purchase-orders/details/:purchaseOrderId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/purchase-orders');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/purchase-orders/:action/:purchaseOrderId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit purchase order';
            controller.find();
        } else {
            controller.pageHeader = 'New purchase order';
            controller.PurchaseOrder.create();
        }

        controller.searchStores();
        controller.searchWallets();
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.purchaseOrderForm.$valid && !controller.saving) {
            controller.saving = true;

            controller.PurchaseOrder
                .save(controller.purchaseOrder)
                .then(function (response) {
                    controller.path.redirectTo('/purchase-orders/edit/:purchaseOrderId/supplier',
                        response.data);
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.saving = false;
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.purchaseOrderForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.searchStores = function () {
        var controller = this;
        controller.searchingStores = true;

        controller.Store
            .list()
            .then(function (response) {
                controller.storeList = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingStores = false;
            });
    };

    Controller.prototype.searchWallets = function () {
        var controller = this;
        controller.searchingWallets = true;

        controller.Wallet
            .listActives()
            .then(function (response) {
                controller.walletList = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingWallets = false;
            });
    };

    angular.module('dyoub.app').controller('PurchaseOrderEditController', [
        'HandleError',
        'Path',
        'PurchaseOrder',
        'Store',
        'Wallet',
        Controller
    ]);

})();
