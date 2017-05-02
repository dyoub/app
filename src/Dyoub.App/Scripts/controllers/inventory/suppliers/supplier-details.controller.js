// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Supplier) {
        this.handleError = handleError;
        this.path = path;
        this.Supplier = Supplier;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.Supplier
            .find(controller.routeParams.supplierId)
            .then(function (response) {
                controller.supplier = response.data;
                controller.notFound = !controller.supplier;
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
            .map('/suppliers/details/:supplierId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Supplier
            .remove(controller.routeParams.supplierId)
            .then(function () {
                controller.path.redirectTo('/suppliers');
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

    angular.module('dyoub.app').controller('SupplierDetailsController', [
        'HandleError',
        'Path',
        'Supplier',
        Controller
    ]);

})();
