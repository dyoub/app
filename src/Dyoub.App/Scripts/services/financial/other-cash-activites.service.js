// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {};
    };

    Service.prototype.find = function (otherCashActivityId) {
        return this.$http.post('/other-cash-activities/find', { id: otherCashActivityId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/other-cash-activities', {
            description: filter.description,
            storeId: filter.store ? filter.store.id : null,
            startDate: filter.startDate,
            endDate: filter.endDate,
            index: filter.index,
        });
    };

    Service.prototype.remove = function (otherCashActivityId) {
        return this.$http.post('/other-cash-activities/delete', { id: otherCashActivityId });
    };

    Service.prototype.save = function (otherCashActivity) {
        var action = otherCashActivity.id ? '/other-cash-activities/update' : '/other-cash-activities/create';

        return this.$http.post(action, {
            id: otherCashActivity.id,
            storeId: otherCashActivity.store.id,
            description: otherCashActivity.description,
            date: otherCashActivity.date,
            value: otherCashActivity.value,
            operation: otherCashActivity.operation
        });
    };

    angular.module('dyoub.app').service('OtherCashActivity', [
        '$http',
        Service
    ]);

})();
