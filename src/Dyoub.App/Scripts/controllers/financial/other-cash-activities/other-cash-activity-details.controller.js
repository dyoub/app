// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(OtherCashActivity, handleError, path) {
        this.OtherCashActivity = OtherCashActivity;
        this.handleError = handleError;
        this.path = path;
    }

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

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.removeDialogOpened = false;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/other-cash-activities/details/:otherCashActivityId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.OtherCashActivity
            .remove(controller.routeParams.otherCashActivityId)
            .then(function () {
                controller.path.redirectTo('/other-cash-activities');
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

    angular.module('dyoub.app').controller('OtherCashActivityDetailsController', [
        'OtherCashActivity',
        'HandleError',
        'Path',
        Controller
    ]);

})();
