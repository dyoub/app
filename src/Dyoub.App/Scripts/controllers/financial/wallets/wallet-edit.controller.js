// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Wallet) {
        this.path = path;
        this.handleError = handleError;
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

    Controller.prototype.goBack = function () {
        var controller = this;

        if (controller.routeParams.action === 'edit') {
            controller.path.redirectTo('/wallets/details/:walletId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/wallets');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/wallets/:action/:walletId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit wallet';
            controller.find();
        } else {
            controller.pageHeader = 'New wallet';
            controller.wallet = controller.Wallet.create();
        }
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Wallet
                .save(controller.wallet)
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
        return controller.saving || controller.walletForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('WalletEditController', [
        'HandleError',
        'Path',
        'Wallet',
        Controller
    ]);

})();
