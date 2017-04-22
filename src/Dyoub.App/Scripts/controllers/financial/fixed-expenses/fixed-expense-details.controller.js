// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(FixedExpense, handleError, path) {
        this.FixedExpense = FixedExpense;
        this.handleError = handleError;
        this.path = path;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.FixedExpense
            .find(controller.routeParams.fixedExpenseId)
            .then(function (response) {
                controller.fixedExpense = response.data;
                controller.notFound = !controller.fixedExpense;
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
        controller.routeParams = controller.path.map('/fixed-expenses/details/:fixedExpenseId');
        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.FixedExpense
            .remove(controller.routeParams.fixedExpenseId)
            .then(function () {
                controller.path.redirectTo('/fixed-expenses');
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

    angular.module('dyoub.app').controller('FixedExpenseDetailsController', [
        'FixedExpense',
        'HandleError',
        'Path',
        Controller
    ]);

})();
