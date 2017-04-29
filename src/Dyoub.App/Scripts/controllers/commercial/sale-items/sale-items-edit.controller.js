// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, PricingTable, SaleItems) {
        this.path = path;
        this.dialog = dialog;
        this.handleError = handleError;
        this.PricingTable = PricingTable;
        this.SaleItems = SaleItems;
    }

    Controller.prototype.addItem = function (item) {
        var controller = this;

        var alreadyAdded = controller.itemList.where(function (selectedItem) {
            return selectedItem.id === item.id &&
                selectedItem.isProduct === item.isProduct &&
                selectedItem.isService === item.isService;
        })
        .any();

        if (alreadyAdded) {
            controller.dialog.error('Item already added.');
        } else {
            item.quantity = 1;
            controller.calculateItemTotalPrice(item);
            controller.itemList.push(item);
        }
    };

    Controller.prototype.addFirstItem = function () {
        var controller = this;

        if (controller.itemsForSale.any() && controller.itemSuggestionOpened) {
            var firstItem = controller.itemsForSale.first();
            controller.addItem(firstItem);
        }
    };

    Controller.prototype.calculateItemTotalPrice = function (item) {
        if (typeof item.quantity !== 'number') {
            return 0;
        }

        var discount = item.discount || 0;

        return (item.quantity * item.unitPrice - discount).round(2);
    };

    Controller.prototype.calculateTotalPayable = function () {
        var controller = this,
            totalPayable = 0;

        angular.forEach(controller.itemList, function (item) {
            totalPayable += controller.calculateItemTotalPrice(item);
        });

        return totalPayable;
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.itemToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = this.path
            .map('/sale-orders/edit/:saleOrderId/items');

        controller.searchSelectedItems();
    };

    Controller.prototype.newItemForSaleSearch = function () {
        var controller = this;
        controller.itemSuggestionOpened = true;
        controller.searchingItemsForSale = true;
        controller.itemsForSale = [];
    };

    Controller.prototype.noItemsForSale = function () {
        var controller = this;
        return controller.itemsForSale &&
            controller.itemsForSale.isEmpty() &&
            !controller.searchingItemsForSale;
    };

    Controller.prototype.noItemsSelected = function () {
        var controller = this;
        return controller.itemList &&
            controller.itemList.isEmpty() &&
            !controller.searchingSelectedItems;
    };

    Controller.prototype.removeItem = function () {
        var controller = this,
            itemIndex = controller.itemList.indexOf(controller.itemToRemove);

        controller.itemList.splice(itemIndex, 1);
        controller.itemToRemove = null;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.SaleItems.save({
            saleOrderId: controller.routeParams.saleOrderId,
            items: controller.itemList
        })
        .then(function () {
            controller.path.redirectTo('/sale-orders/edit/:saleOrderId/payments',
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
               controller.saleOrderItemsForm.$invalid ||
               !controller.itemList ||
                controller.itemList.isEmpty();
    };

    Controller.prototype.searchItemsForSale = function () {
        var controller = this;
        controller.searchingItemsForSale = true;

        controller.PricingTable.listItemsForSale({
            storeId: controller.storeId,
            nameOrCode: controller.itemSearch.nameOrCode
        })
        .then(function (response) {
            controller.itemsForSale = response.data;
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searchingItemsForSale = false;
        });
    };

    Controller.prototype.searchSelectedItems = function () {
        var controller = this;
        controller.searchingSelectedItems = true;

        controller.SaleItems
            .list(controller.routeParams.saleOrderId)
            .then(function (response) {
                controller.storeId = response.data.storeId;
                controller.itemList = response.data.itemList;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searchingSelectedItems = false;
            });
    };

    Controller.prototype.searchSelectedItemsComplete = function () {
        var controller = this;
        return !(controller.searchingSelectedItems || controller.error);
    };

    Controller.prototype.showRemoveItemDialog = function (item) {
        var controller = this;
        controller.itemToRemove = item;
    };

    angular.module('dyoub.app').controller('SaleItemsEditController', [
        'Dialog',
        'HandleError',
        'Path',
        'PricingTable',
        'SaleItems',
        Controller
    ]);

})();
