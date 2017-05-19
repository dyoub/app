// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, RentedProducts) {
        this.path = path;
        this.handleError = handleError;
        this.RentedProducts = RentedProducts;
    }

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/rent-contracts/details/:rentContractId/items');

        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.RentedProducts
            .list(controller.routeParams.rentContractId)
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

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('RentedProductsDetailsController', [
        'HandleError',
        'Path',
        'RentedProducts',
        Controller
    ]);

})();
