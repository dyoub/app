// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.daily = function (filter) {
        return this.$http.post('/cash-flow/monthly', {
            storeId: filter.storeId,
            month: filter.month,
            year: filter.year
        });
    };

    Service.prototype.monthly = function (filter) {
        return this.$http.post('/cash-flow/monthly', {
            storeId: filter.storeId,
            year: filter.year
        });
    };

    angular.module('dyoub.app').service('FixedExpense', [
        '$http',
        Service
    ]);

})();
