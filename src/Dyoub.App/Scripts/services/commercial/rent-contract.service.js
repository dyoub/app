// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return { startDate: new Date() };
    };

    Service.prototype.confirm = function (rentContractId) {
        return this.$http.post('/rent-contracts/confirm', { id: rentContractId });
    };

    Service.prototype.find = function (rentContractId) {
        return this.$http.post('/rent-contracts/find', { id: rentContractId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/rent-contracts', filter);
    };

    Service.prototype.remove = function (rentContractId) {
        return this.$http.post('/rent-contracts/delete', { id: rentContractId });
    };

    Service.prototype.revert = function (rentContractId) {
        return this.$http.post('/rent-contracts/revert', { id: rentContractId });
    };

    Service.prototype.save = function (rentContract) {
        var action = rentContract.id ? '/rent-contracts/update' : '/rent-contracts/create';

        return this.$http.post(action, {
            id: rentContract.id,
            storeId: rentContract.store.id,
            walletId: rentContract.wallet ? rentContract.wallet.id : null,
            startDate: rentContract.startDate,
            startTime: rentContract.startTime,
            endDate: rentContract.endDate,
            endTime: rentContract.endTime,
            location: rentContract.location,
            additionalInformation: rentContract.additionalInformation
        });
    };

    angular.module('dyoub.app').service('RentContract', [
        '$http',
        Service
    ]);

})();
