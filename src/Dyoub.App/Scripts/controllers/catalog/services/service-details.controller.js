// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Service, handleError, path) {
        this.Service = Service;
        this.handleError = handleError;
        this.path = path;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller
            .Service.find(controller.routeParams.serviceId)
            .then(function (response) {
                controller.service = response.data;
                controller.notFound = !controller.service;
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
            .map('/services/details/:serviceId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Service
            .remove(controller.routeParams.serviceId)
            .then(function () {
                controller.path.redirectTo('/services');
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

    angular.module('dyoub.app').controller('ServiceDetailsController', [
        'Service',
        'HandleError',
        'Path',
        Controller
    ]);

})();
