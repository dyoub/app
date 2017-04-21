// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Customer, handleError, path) {
        this.Customer = Customer;
        this.handleError = handleError;
        this.path = path;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.Customer
            .find(controller.routeParams.customerId)
            .then(function (response) {
                controller.customer = response.data;
                controller.notFound = !controller.customer;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/customers/details/:customerId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Customer
            .remove(controller.routeParams.customerId)
            .then(function () {
                controller.path.redirectTo('/customers');
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.hideRemoveDialog();
            });
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.showRemoveDialog = function () {
        var controller = this;
        controller.removing = false;
        controller.removeDialogOpened = true;
    };

    angular.module('dyoub.app').controller('CustomerDetailsController', [
        'Customer',
        'HandleError',
        'Path',
        Controller
    ]);

})();
