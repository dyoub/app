// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, PricingTable) {
        this.handleError = handleError;
        this.PricingTable = PricingTable;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {};
    };

    Controller.prototype.init = function () {
        var controller = this;
        controller.clearFilter();
        controller.newSearch();
    };

    Controller.prototype.newSearch = function () {
        var controller = this;
        controller.pricingTableList = [];
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.PricingTable
            .list(controller.filter)
            .then(function (response) {
                controller.pricingTableList.pushRange(response.data);
                controller.filter.index = controller.pricingTableList.length;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    angular.module('dyoub.app').controller('PricingTableListController', [
        'HandleError',
        'PricingTable',
        Controller
    ]);

})();
