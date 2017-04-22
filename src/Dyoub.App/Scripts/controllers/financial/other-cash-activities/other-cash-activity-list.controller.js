// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(OtherCashActivity, handleError, Store) {
        this.OtherCashActivity = OtherCashActivity;
        this.handleError = handleError;
        this.Store = Store;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this,
            today = new Date();

        controller.filter = {
            startDate: new Date(today.getFullYear(), today.getMonth(), 1),
            endDate: new Date(today.getFullYear(), today.getMonth(), today.getLastDate())
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
        controller.otherCashActivityList = [];
        controller.filter.index = 0;
        controller.search();
    };

    Controller.prototype.noRecordsFound = function () {
        var controller = this;
        return controller.otherCashActivityList.isEmpty() && !controller.searching;
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.OtherCashActivity
            .list(controller.filter)
            .then(function (response) {
                controller.otherCashActivityList.pushRange(response.data);
                controller.filter.index = controller.otherCashActivityList.length
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
        return controller.searching || controller.otherCashActivitysFilter.$invalid;
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

    angular.module('dyoub.app').controller('OtherCashActivityListController', [
        'OtherCashActivity',
        'HandleError',
        'Store',
        Controller
    ]);

})();
