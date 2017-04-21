// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(PaymentMethod, handleError) {
        this.PaymentMethod = PaymentMethod;
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
        controller.paymentMethodList = [];
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.PaymentMethod
            .list(controller.filter)
            .then(function (response) {
                controller.paymentMethodList.pushRange(response.data);
                controller.filter.index = controller.paymentMethodList.length;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    angular.module('dyoub.app').controller('PaymentMethodListController', [
        'PaymentMethod',
        'HandleError',
        Controller
    ]);

})();
