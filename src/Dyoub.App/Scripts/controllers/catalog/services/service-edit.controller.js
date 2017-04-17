// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Service) {
        this.handleError = handleError;
        this.path = path;
        this.Service = Service;
    }

    Controller.prototype.find = function () {
        var controller = this;

        controller.searching = true;

        controller.Service
            .find(controller.routeParams.serviceId)
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

    Controller.prototype.goBack = function () {
        var controller = this;

        if (controller.routeParams.action === 'edit') {
            controller.path.redirectTo('/services/details/:serviceId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/services');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/services/:action/:serviceId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit service';
            controller.find();
        } else {
            controller.pageHeader = 'New service';
            controller.service = controller.Service.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Service
                .save(controller.service)
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
        return controller.saving || controller.serviceForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('ServiceEditController', [
        'HandleError',
        'Path',
        'Service',
        Controller
    ]);

})();
