// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, RentContract, Store) {
        this.handleError = handleError;
        this.RentContract = RentContract;
        this.Store = Store;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this,
            today = new Date();

        controller.filter = {
            fromDate: new Date(today.getFullYear(), today.getMonth(), 1),
            toDate: new Date(today.getFullYear(), today.getMonth(), today.getLastDate())
        };
    };

    Controller.prototype.init = function () {
        var controller = this;
        controller.clearFilter();
        controller.newSearch();
        controller.searchStores();
    };

    Controller.prototype.newSearch = function () {
        var controller = this;
        controller.rentContractHistory = [];
        controller.count = 0;
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.RentContract.list({
            storeId: controller.filter.store ? controller.filter.store.id : null,
            fromDate: controller.filter.fromDate,
            toDate: controller.filter.toDate,
            index: controller.rentContractHistory.length
        })
        .then(function (response) {
            controller.rentContractHistory.pushRange(response.data);
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searching = false;
        });
    };

    Controller.prototype.searchBlocked = function () {
        var controller = this;
        return controller.rentContractsFilter.$invalid || controller.searching;
    };

    Controller.prototype.searchStores = function () {
        var controller = this;
        controller.searchingStores = true;

        controller.Store
            .list()
            .then(function (response) {
                controller.storeList = response.data;
                controller.notStoresYet = controller.storeList.length === 0;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingStores = false;
            });
    };

    angular.module('dyoub.app').controller('RentContractListController', [
        'HandleError',
        'RentContract',
        'Store',
        Controller
    ]);

})();
