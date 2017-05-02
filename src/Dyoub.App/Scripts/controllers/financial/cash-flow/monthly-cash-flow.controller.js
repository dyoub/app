// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(CashFlow, handleError, Store) {
        this.CashFlow = CashFlow;
        this.handleError = handleError;
        this.Store = Store;
    }

    Controller.prototype.init = function () {
        var controller = this;
        controller.clearFilter();
        controller.newSearch();
        controller.searchStores();
    };

    Controller.prototype.clearFilter = function () {
        var controller = this,
            today = new Date();

        controller.filter = {
            year: today.getFullYear()
        };
    };

    Controller.prototype.newSearch = function () {
        var controller = this;
        controller.monthlyCashFlow = [];
        controller.count = 0;
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.CashFlow.monthly({
            storeId: controller.filter.store ? controller.filter.store.id : null,
            year: controller.filter.year
        })
        .then(function (response) {
            controller.monthlyCashFlow = response.data;
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
        return controller.searching || controller.filterForm.$invalid;
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

    angular.module('dyoub.app').controller('MonthlyCashFlowController', [
        'CashFlow',
        'HandleError',
        'Store',
        Controller
    ]);

})();
