// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, Wallet) {
        this.handleError = handleError;
        this.Wallet = Wallet;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {};
    };

    Controller.prototype.init = function () {
        var controller = this;
        controller.clearFilter();
        controller.newSearch();
    };

    Controller.prototype.newSearch = function () {
        var controller = this;
        controller.walletList = [];
        controller.filter.index = 0;
        controller.search();
    };

    Controller.prototype.noRecordsFound = function () {
        var controller = this;
        return controller.walletList.isEmpty() && !controller.searching;
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.Wallet
            .list(controller.filter)
            .then(function (response) {
                controller.walletList.pushRange(response.data);
                controller.filter.index = controller.walletList.length
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.searchBlocked = function () {
        var controller = this;
        return controller.searching || controller.walletsFilter.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('WalletListController', [
        'HandleError',
        'Wallet',
        Controller
    ]);

})();
