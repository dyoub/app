// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Customer, handleError, path, RentContractCustomer) {
        this.path = path;
        this.handleError = handleError;
        this.Customer = Customer;
        this.RentContractCustomer = RentContractCustomer;
    }

    Controller.prototype.clearCustomerData = function () {
        var controller = this;
        controller.rentContract.customer = null;
    };

    Controller.prototype.hasCustomerData = function () {
        var controller = this;

        if (!controller.rentContract || !controller.rentContract.customer) {
            return false;
        }

        for (var property in controller.rentContract.customer) {
            if (controller.rentContract.customer[property]) {
                return true;
            }
        }

        return false;
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.RentContractCustomer
            .find(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.getCustomer = function () {
        var controller = this;

        if (!controller.hasCustomerData()) {
            return null;
        }

        return controller.rentContract.customer
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/rent-contracts/edit/:rentContractId/customer');

        controller.find();
    };

    Controller.prototype.newCustomerSearch = function () {
        var controller = this;
        controller.customerSuggestionOpened = true;
        controller.searchingCustomers = true;
        controller.customerList = [];
    };

    Controller.prototype.noCustomers = function () {
        var controller = this;
        return controller.customerList &&
            controller.customerList.isEmpty() &&
            !controller.searchingCustomers;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.rentContractCustomerForm.$valid && !controller.saving) {
            controller.saving = true;

            controller.RentContractCustomer.save({
                rentContractId: controller.routeParams.rentContractId,
                customer: controller.getCustomer()
            })
            .then(function (response) {
                controller.path.redirectTo('/rent-contracts/edit/:rentContractId/products',
                    controller.routeParams);
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.saving = false;
            });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.rentContractCustomerForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.searchCustomers = function () {
        var controller = this;
        controller.searchingCustomers = true;

        controller.Customer.list({
            name: controller.rentContract.customer.name,
            index: 0
        })
        .then(function (response) {
            controller.customerList = response.data;
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searchingCustomers = false;
        });
    };

    Controller.prototype.selectCustomer = function (customer) {
        var controller = this;
        controller.rentContract.customer = customer;
        controller.customerSuggestionOpened = false;
    };

    angular.module('dyoub.app').controller('RentContractCustomerEditController', [
        'Customer',
        'HandleError',
        'Path',
        'RentContractCustomer',
        Controller
    ]);

})();
