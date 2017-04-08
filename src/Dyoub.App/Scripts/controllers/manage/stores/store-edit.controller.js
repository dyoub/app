// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Store) {
        this.handleError = handleError;
        this.path = path;
        this.Store = Store;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.Store
            .find(controller.routeParams.storeId)
            .then(function (response) {
                controller.store = response.data;
                controller.notFound = !controller.store;
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
            controller.path.redirectTo('/stores/details/:storeId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/stores');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/stores/:action/:storeId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit store';
            controller.find();
        } else {
            controller.pageHeader = 'New store';
            controller.store = controller.Store.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Store
                .save(controller.store)
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
        return controller.saving || controller.storeForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('StoreEditController', [
        'HandleError',
        'Path',
        'Store',
        Controller
    ]);

})();
