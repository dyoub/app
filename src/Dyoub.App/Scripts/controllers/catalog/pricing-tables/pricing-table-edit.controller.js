// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, PricingTable) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.PricingTable = PricingTable;
    }

    Controller.prototype.addItemToChanges = function (item) {
        var controller = this,
            itemIndex = controller.changes.indexOf(item),
            itemAddedToChanges = itemIndex >= 0;

        item.priceChanged = item.unitRentPrice !== item.oldUnitRentPrice ||
            item.unitSalePrice !== item.oldUnitSalePrice;

        if (item.priceChanged && !itemAddedToChanges) {
            controller.changes.push(item);
        }

        if (!item.priceChanged && itemAddedToChanges) {
            controller.changes.splice(itemIndex, 1);
        }
    };

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {
            storeId: controller.routeParams.storeId,
            searchFor: 'All'
        };
    };

    Controller.prototype.confirmUndoChanges = function () {
        var controller = this;

        if (controller.changes.isEmpty()) {
            controller.dialog.success('There is no changes.');
        } else {
            controller.undoChangesDialogOpened = true;
        }
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
                controller.error = true;
            })
            ['finally'](function () {
                controller.finding = false;
            });
    };

    Controller.prototype.findComplete = function () {
        var controller = this;
        return !(controller.finding || controller.tableError);
    };

    Controller.prototype.hideUndoChangesDialog = function () {
        var controller = this;
        controller.undoChangesDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/pricing-tables/details/:storeId');

        controller.changes = [];
        controller.clearFilter();
        controller.find();
    };

    Controller.prototype.newSearchOfItems = function () {
        var controller = this;
        controller.filter.index = 0;
        controller.itemList = [];
        controller.searchItems();
    };

    Controller.prototype.save = function () {
        var controller = this;
        controller.saving = true;

        controller.PricingTable
            .save(controller.routeParams.storeId, controller.changes)
            .then(function () {
                controller.path.redirectTo('/pricing-tables/details/:storeId', controller.routeParams);
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.saving = false;
            });
    };

    Controller.prototype.searchItems = function () {
        var controller = this;

        if (controller.store) {
            controller.searchingItems = true;

            controller.PricingTable
                .listItems(controller.filter)
                .then(function (response) {
                    controller.itemList.pushRange(controller.sync(response.data));
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.error = true;
                })
                ['finally'](function () {
                    controller.searchingItems = false;
                });
        }
    };

    Controller.prototype.searchItemsComplete = function () {
        var controller = this;
        return !(controller.searchingItems || controller.itemsError);
    };

    Controller.prototype.showUndoChangesDialog = function () {
        var controller = this;
        controller.undoChangesDialogOpened = true;
    };

    Controller.prototype.sync = function (items) {
        var controller = this;

        return items.select(function (item) {
            var itemFromChanges = controller.changes.where(function (changedItem) {
                return changedItem.id == item.id &&
                       changedItem.isProduct == item.isProduct &&
                       changedItem.isService == item.isService;
            })
            .first();

            if (itemFromChanges) {
                itemFromChanges.name = item.name;
                itemFromChanges.code = item.code;
                itemFromChanges.oldUnitRentPrice = item.unitRentPrice;
                itemFromChanges.oldUnitSalePrice = item.unitSalePrice;

                return itemFromChanges;
            }

            item.oldUnitRentPrice = item.unitRentPrice;
            item.oldUnitSalePrice = item.unitSalePrice;
            item.priceChanged = false;

            return item;
        });
    };

    Controller.prototype.undoChanges = function () {
        var controller = this;

        angular.forEach(controller.changes, function (item) {
            item.unitRentPrice = item.oldUnitRentPrice;
            item.unitSalePrice = item.oldUnitSalePrice;
            item.priceChanged = false;
        });

        controller.changes = [];
        controller.undoChangesDialogOpened = false;
        controller.dialog.success('Changes undone.');
    };

    angular.module('dyoub.app').controller('PricingTableEditController', [
        'Dialog',
        'HandleError',
        'Path',
        'PricingTable',
        Controller
    ]);

})();
