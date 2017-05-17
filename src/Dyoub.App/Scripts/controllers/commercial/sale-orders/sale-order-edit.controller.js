// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, SaleOrder, Store, Wallet) {
        this.path = path;
        this.handleError = handleError;
        this.SaleOrder = SaleOrder;
        this.Store = Store;
        this.Wallet = Wallet;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.SaleOrder
            .find(controller.routeParams.saleOrderId)
            .then(function (response) {
                controller.saleOrder = response.data;
                controller.notFound = !controller.saleOrder;
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
            controller.path.redirectTo('/sale-orders/details/:saleOrderId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/sale-orders');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/sale-orders/:action/:saleOrderId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit sale order';
            controller.find();
        } else {
            controller.pageHeader = 'New sale order';
            controller.SaleOrder.create();
        }

        controller.searchStores();
        controller.searchWallets();
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saleOrderForm.$valid && !controller.saving) {
            controller.saving = true;

            controller.SaleOrder
                .save(controller.saleOrder)
                .then(function (response) {
                    controller.path.redirectTo('/sale-orders/edit/:saleOrderId/customer',
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
        return controller.saving || controller.saleOrderForm.$invalid;
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

    angular.module('dyoub.app').controller('SaleOrderEditController', [
        'HandleError',
        'Path',
        'SaleOrder',
        'Store',
        'Wallet',
        Controller
    ]);

})();
