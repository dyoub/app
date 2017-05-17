// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return { startDate: new Date() };
    };

    Service.prototype.find = function (saleOrderId) {
        return this.$http.post('/rent-contracts/find', { id: saleOrderId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/rent-contracts', filter);
    };

    Service.prototype.remove = function (saleOrderId) {
        return this.$http.post('/rent-contracts/delete', { id: saleOrderId });
    };

    Service.prototype.save = function (saleOrder) {
        var action = saleOrder.id ? '/rent-contracts/update' : '/rent-contracts/create';

        return this.$http.post(action, {
            id: saleOrder.id,
            storeId: saleOrder.store.id,
            walletId: saleOrder.wallet ? saleOrder.wallet.id : null,
            startDate: saleOrder.startDate,
            endDate: saleOrder.endDate,
            location: saleOrder.location,
            additionalInformation: saleOrder.additionalInformation
        });
    };

    angular.module('dyoub.app').service('RentContract', [
        '$http',
        Service
    ]);

})();
