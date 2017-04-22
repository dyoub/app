// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(OtherCashActivity, handleError, Store, path) {
        this.OtherCashActivity = OtherCashActivity;
        this.Store = Store;
        this.path = path;
        this.handleError = handleError;
    }

    Controller.prototype.create = function () {
        var controller = this;
        controller.otherCashActivity = {};
    };

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.OtherCashActivity
            .find(controller.routeParams.otherCashActivityId)
            .then(function (response) {
                controller.otherCashActivity = response.data;
                controller.notFound = !controller.otherCashActivity;
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
            controller.path.redirectTo('/other-cash-activities/details/:otherCashActivityId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/other-cash-activities');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/other-cash-activities/:action/:otherCashActivityId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit cash activity';
            controller.find();
        } else {
            controller.pageHeader = 'New cash activity';
            controller.create();
        }

        controller.searchStores();
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.OtherCashActivity
                .save(controller.otherCashActivity)
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
        return controller.saving || controller.otherCashActivityForm.$invalid;
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

    angular.module('dyoub.app').controller('OtherCashActivityEditController', [
        'OtherCashActivity',
        'HandleError',
        'Store',
        'Path',
        Controller
    ]);

})();
