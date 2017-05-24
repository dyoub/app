// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Partner) {
        this.handleError = handleError;
        this.path = path;
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

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/partners/details/:partnerId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Partner
            .remove(controller.routeParams.partnerId)
            .then(function () {
                controller.path.redirectTo('/partners');
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

    angular.module('dyoub.app').controller('PartnerDetailsController', [
        'HandleError',
        'Path',
        'Partner',
        Controller
    ]);

})();
