// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, RentContract, Store, Wallet) {
        this.path = path;
        this.handleError = handleError;
        this.RentContract = RentContract;
        this.Store = Store;
        this.Wallet = Wallet;
    }

    Controller.prototype.find = function () {
        var controller = this;
        controller.searching = true;

        controller.RentContract
            .find(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;
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
            controller.path.redirectTo('/rent-contracts/details/:rentContractId',
                controller.routeParams);
        } else {
            controller.path.redirectTo('/rent-contracts');
        }
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/rent-contracts/:action/:rentContractId');

        if (controller.routeParams.action === 'edit') {
            controller.pageHeader = 'Edit rent contract';
            controller.find();
        } else {
            controller.pageHeader = 'New rent contract';
            controller.RentContract.create();
        }

        controller.searchStores();
        controller.searchWallets();
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.rentContractForm.$valid && !controller.saving) {
            controller.saving = true;

            controller.RentContract
                .save(controller.rentContract)
                .then(function (response) {
                    controller.path.redirectTo('/rent-contracts/edit/:rentContractId/customer',
                        response.data);
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.saving = false;
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.rentContractForm.$invalid;
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    Controller.prototype.searchStores = function () {
        var controller = this;
        controller.searchingStores = true;

        controller.Store
            .list()
            .then(function (response) {
                controller.storeList = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingStores = false;
            });
    };

    Controller.prototype.searchWallets = function () {
        var controller = this;
        controller.searchingWallets = true;

        controller.Wallet
            .listActives()
            .then(function (response) {
                controller.walletList = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingWallets = false;
            });
    };

    angular.module('dyoub.app').controller('RentContractEditController', [
        'HandleError',
        'Path',
        'RentContract',
        'Store',
        'Wallet',
        Controller
    ]);

})();
