// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PricingTable, Store) {
        this.handleError = handleError;
        this.path = path;
        this.PricingTable = PricingTable;
        this.Store = Store;
    }

    Controller.prototype.copyPrices = function () {
        var controller = this;

        if (!controller.copyPricesBlocked()) {
            controller.copying = true;

            var fromStoreId = controller.storeToCopy.id,
                toStoreId = controller.routeParams.storeId;

            controller.PricingTable
                .copy(fromStoreId, toStoreId)
                .then(function () {
                    controller.path.redirectTo('/pricing-tables/details/:storeId', controller.routeParams);
                })
                ['catch'](function (response) {
                    controller.hideCopyDialog();
                    controller.handleError(response);
                    controller.copying = false;
                });
        }
    };

    Controller.prototype.copyPricesBlocked = function () {
        var controller = this;
        return controller.copying ||
               controller.formCopyPricingTable.$invalid ||
               controller.selectedSameStore();
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.PricingTable
            .find(controller.routeParams.storeId)
            .then(function (response) {
                controller.store = response.data;
                controller.notFound = !controller.store;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.hideCopyDialog = function () {
        var controller = this;
        controller.copyDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;
        controller.routeParams = controller.path.map('/pricing-tables/copy-to/:storeId');
        controller.find();
        controller.searchStores();
    };

    Controller.prototype.selectedSameStore = function () {
        var controller = this;
        return !!controller.storeToCopy && controller.routeParams.storeId == controller.storeToCopy.id;
    };

    Controller.prototype.searchStores = function () {
        var controller = this;
        controller.searchingStores = true;

        controller.Store.list()
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

    Controller.prototype.showCopyDialog = function () {
        var controller = this;
        controller.copyDialogOpened = true;
    };

    angular.module('dyoub.app').controller('PricingTableCopyController', [
        'HandleError',
        'Path',
        'PricingTable',
        'Store',
        Controller
    ]);

})();
