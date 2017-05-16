// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.find = function (storeId) {
        return this.$http.post('/product-stock/find', { storeId: storeId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/product-stock', filter);
    };

    Service.prototype.listProducts = function (filter) {
        return this.$http.post('/product-stock/quantities', filter);
    };

    angular.module('dyoub.app').service('ProductStock', [
        '$http',
        Service
    ]);

})();
