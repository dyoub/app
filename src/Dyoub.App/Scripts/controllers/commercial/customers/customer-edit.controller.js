// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Customer, handleError, path) {
        this.Customer = Customer;
        this.path = path;
        this.handleError = handleError;
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

    Controller.prototype.goBack = function () {
        var controller = this;

        if (controller.routeParams.action === 'edit') {
            controller.path.redirectTo('/customers/details/:customerId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/customers');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/customers/:action/:customerId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit customer';
            controller.find();
        } else {
            controller.pageHeader = 'New customer';
            controller.customer = controller.Customer.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Customer
                .save(controller.customer)
                .then(function () {
                    controller.goBack();
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.saving = false;
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.customerForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('CustomerEditController', [
        'Customer',
        'HandleError',
        'Path',
        Controller
    ]);

})();
