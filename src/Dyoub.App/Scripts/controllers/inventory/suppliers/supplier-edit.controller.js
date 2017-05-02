// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Supplier) {
        this.path = path;
        this.handleError = handleError;
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

    Controller.prototype.goBack = function () {
        var controller = this;

        if (controller.routeParams.action === 'edit') {
            controller.path.redirectTo('/suppliers/details/:supplierId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/suppliers');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/suppliers/:action/:supplierId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit supplier';
            controller.find();
        } else {
            controller.pageHeader = 'New supplier';
            controller.supplier = controller.Supplier.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Supplier
                .save(controller.supplier)
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
        return controller.saving || controller.supplierForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('SupplierEditController', [
        'HandleError',
        'Path',
        'Supplier',
        Controller
    ]);

})();
