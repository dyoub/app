// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Customer, handleError) {
        this.Customer = Customer;
        this.handleError = handleError;
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
        controller.filter.index = 0;
        controller.customerList = [];
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.Customer
            .list(controller.filter)
            .then(function (response) {
                controller.customerList.pushRange(response.data);
                controller.filter.index = controller.customerList.length
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    angular.module('dyoub.app').controller('CustomerListController', [
        'Customer',
        'HandleError',
        Controller
    ]);

})();
