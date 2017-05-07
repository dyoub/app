// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Customer, handleError, path, SaleCustomer) {
        this.path = path;
        this.handleError = handleError;
        this.Customer = Customer;
        this.SaleCustomer = SaleCustomer;
    }

    Controller.prototype.clearCustomerData = function () {
        var controller = this;
        controller.saleOrder.customer = null;
    };

    Controller.prototype.hasCustomerData = function () {
        var controller = this;

        if (!controller.saleOrder || !controller.saleOrder.customer) {
            return false;
        }

        for (var property in controller.saleOrder.customer) {
            if (controller.saleOrder.customer[property]) {
                return true;
            }
        }

        return false;
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.SaleCustomer
            .find(controller.routeParams.saleOrderId)
            .then(function (response) {
                controller.saleOrder = response.data;
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

        return controller.saleOrder.customer
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/sale-orders/edit/:saleOrderId/customer');

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

        if (controller.saleCustomerForm.$valid && !controller.saving) {
            controller.saving = true;

            controller.SaleCustomer.save({
                saleOrderId: controller.routeParams.saleOrderId,
                customer: controller.getCustomer()
            })
            .then(function (response) {
                controller.path.redirectTo('/sale-orders/edit/:saleOrderId/items',
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
        return controller.saving || controller.saleCustomerForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.searchCustomers = function () {
        var controller = this;
        controller.searchingCustomers = true;

        controller.Customer.list({
            name: controller.saleOrder.customer.name,
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
        controller.saleOrder.customer = customer;
        controller.customerSuggestionOpened = false;
    };

    angular.module('dyoub.app').controller('SaleCustomerEditController', [
        'Customer',
        'HandleError',
        'Path',
        'SaleCustomer',
        Controller
    ]);

})();
