// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {};
    };

    Service.prototype.find = function (fixedExpenseId) {
        return this.$http.post('/fixed-expenses/find', { id: fixedExpenseId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/fixed-expenses', filter);
    };

    Service.prototype.remove = function (fixedExpenseId) {
        return this.$http.post('/fixed-expenses/delete', { id: fixedExpenseId });
    };

    Service.prototype.save = function (fixedExpense) {
        var action = fixedExpense.id ? '/fixed-expenses/update' : '/fixed-expenses/create';

        return this.$http.post(action, {
            id: fixedExpense.id,
            storeId: fixedExpense.store.id,
            description: fixedExpense.description,
            startDate: fixedExpense.startDate,
            endDate: fixedExpense.endDate,
            value: fixedExpense.value
        });
    };

    angular.module('dyoub.app').service('FixedExpense', [
        '$http',
        Service
    ]);

})();
