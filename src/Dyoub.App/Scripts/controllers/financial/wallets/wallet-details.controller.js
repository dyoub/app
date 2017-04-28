// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Wallet) {
        this.handleError = handleError;
        this.path = path;
        this.Wallet = Wallet;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.Wallet
            .find(controller.routeParams.walletId)
            .then(function (response) {
                controller.wallet = response.data;
                controller.notFound = !controller.wallet;
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
            .map('/wallets/details/:walletId');

        controller.find();
    };

    Controller.prototype.remove = function () {
        var controller = this;
        controller.removing = true;

        controller.Wallet
            .remove(controller.routeParams.walletId)
            .then(function () {
                controller.path.redirectTo('/wallets');
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

    angular.module('dyoub.app').controller('WalletDetailsController', [
        'HandleError',
        'Path',
        'Wallet',
        Controller
    ]);

})();
