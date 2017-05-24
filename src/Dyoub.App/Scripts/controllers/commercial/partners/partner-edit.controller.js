// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Partner) {
        this.path = path;
        this.handleError = handleError;
        this.Partner = Partner;
    }

    Controller.prototype.find = function () {
        var controller = this;

        controller.searching = true;

        controller.Partner
            .find(controller.routeParams.partnerId)
            .then(function (response) {
                controller.partner = response.data;
                controller.notFound = !controller.partner;
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
            controller.path.redirectTo('/partners/details/:partnerId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/partners');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/partners/:action/:partnerId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit partner';
            controller.find();
        } else {
            controller.pageHeader = 'New partner';
            controller.partner = controller.Partner.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Partner
                .save(controller.partner)
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
        return controller.saving || controller.partnerForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('PartnerEditController', [
        'HandleError',
        'Path',
        'Partner',
        Controller
    ]);

})();
