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

        var alreadyAdded = controller.saleOrder.itemList.where(function (selectedItem) {
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
            controller.saleOrder.itemList.push(item);
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
        var quantity = item.quantity || 0,
            unitPrice = item.unitPrice || 0,
            total = quantity * unitPrice,
            discount = total * (item.discount || 0) / 100;

        return item.total = (total - discount).round(2);
    };

    Controller.prototype.calculateTotalPayable = function () {
        var controller = this;

        if (!controller.saleOrder) return;

        var totalPayable = 0;

        angular.forEach(controller.saleOrder.itemList, function (item) {
            totalPayable += controller.calculateItemTotalPrice(item);
        });

        return totalPayable < 0 ? 0 : totalPayable;
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
        return controller.saleOrder &&
            controller.saleOrder.itemList &&
            controller.saleOrder.itemList.isEmpty() &&
            !controller.searchingSelectedItems;
    };

    Controller.prototype.removeItem = function () {
        var controller = this,
            itemIndex = controller.saleOrder.itemList.indexOf(controller.itemToRemove);

        controller.saleOrder.itemList.splice(itemIndex, 1);
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
            items: controller.saleOrder.itemList
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
               !controller.saleOrder ||
               !controller.saleOrder.itemList ||
                controller.saleOrder.itemList.isEmpty();
    };

    Controller.prototype.searchItemsForSale = function () {
        var controller = this;
        controller.searchingItemsForSale = true;

        controller.PricingTable.listItemsForSale({
            storeId: controller.saleOrder.storeId,
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
                controller.saleOrder = response.data;
                controller.notFound = !controller.saleOrder;
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
