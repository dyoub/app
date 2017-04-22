// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(FixedExpense, handleError, Store, path) {
        this.FixedExpense = FixedExpense;
        this.Store = Store;
        this.path = path;
        this.handleError = handleError;
    }

    Controller.prototype.create = function () {
        var controller = this;
        controller.fixedExpense = {};
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.FixedExpense
            .find(controller.routeParams.fixedExpenseId)
            .then(function (response) {
                controller.fixedExpense = response.data;
                controller.notFound = !controller.fixedExpense;
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
            controller.path.redirectTo('/fixed-expenses/details/:fixedExpenseId', 
                controller.routeParams);
        } else {
            controller.path.redirectTo('/fixed-expenses');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/fixed-expenses/:action/:fixedExpenseId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit fixed expense';
            controller.find();
        } else {
            controller.pageHeader = 'New fixed expense';
            controller.create();
        }

        controller.searchStores();
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.FixedExpense
                .save(controller.fixedExpense)
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
        return controller.saving || controller.fixedExpenseForm.$invalid;
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

    angular.module('dyoub.app').controller('FixedExpenseEditController', [
        'FixedExpense',
        'HandleError',
        'Store',
        'Path',
        Controller
    ]);

})();
