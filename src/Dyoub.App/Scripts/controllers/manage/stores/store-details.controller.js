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

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/stores/details/:storeId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Store
            .remove(controller.routeParams.storeId)
            .then(function () {
                controller.path.redirectTo('/stores');
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.hideRemoveDialog();
            });
    };

    Controller.prototype.showRemoveDialog = function () {
        var controller = this;
        controller.removing = false;
        controller.removeDialogOpened = true;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('StoreDetailsController', [
        'HandleError',
        'Path',
        'Store',
        Controller
    ]);

})();
