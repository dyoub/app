// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, PricingTable) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.PricingTable = PricingTable;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {
            searchFor: 'All',
            storeId: controller.routeParams.storeId
        };
    };

    Controller.prototype.erasePrices = function () {
        var controller = this;

        controller.erasing = true;

        controller.PricingTable
            .clear(controller.routeParams.storeId)
            .then(function () {
                controller.dialog.success('Prices erased.');
                controller.newSearchOfItems();
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function (response) {
                controller.hideClearDialog();
                controller.erasing = false;
            });
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.finding = true;

        controller.PricingTable
            .find(controller.routeParams.storeId)
            .then(function (response) {
                controller.store = response.data;
                controller.notFound = !controller.store;

                if (controller.store) {
                    controller.newSearchOfItems();
                }
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.tableError = true;
            })
            ['finally'](function () {
                controller.finding = false;
            });
    };

    Controller.prototype.findComplete = function () {
        var controller = this;
        return !(controller.finding || controller.tableError);
    };

    Controller.prototype.hideClearDialog = function () {
        var controller = this;
        controller.clearDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/pricing-tables/details/:storeId');

        controller.clearFilter();
        controller.find();
    };

    Controller.prototype.newSearchOfItems = function () {
        var controller = this;
        controller.filter.index = 0;
        controller.itemList = [];
        controller.searchItems();
    };

    Controller.prototype.searchItems = function () {
        var controller = this;
        controller.searchingItems = true;

        controller.PricingTable
            .listItems(controller.filter)
            .then(function (response) {
                controller.itemList.pushRange(response.data);
                controller.filter.index = controller.itemList.length;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.itemsError = true;
            })
            ['finally'](function () {
                controller.searchingItems = false;
            });
    };

    Controller.prototype.searchItemsComplete = function () {
        var controller = this;
        return !(controller.searchingItems || controller.itemsError);
    };

    Controller.prototype.showClearDialog = function () {
        var controller = this;
        controller.clearDialogOpened = true;
    };

    angular.module('dyoub.app').controller('PricingTableDetailsController', [
        'Dialog',
        'HandleError',
        'Path',
        'PricingTable',
        Controller
    ]);

})();
