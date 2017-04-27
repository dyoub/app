// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return { issueDate: new Date() };
    };

    Service.prototype.find = function (saleOrderId) {
        return this.$http.post('/sale-orders/find', { id: saleOrderId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/sale-orders', filter);
    };

    Service.prototype.remove = function (saleOrderId) {
        return this.$http.post('/sale-orders/delete', { id: saleOrderId });
    };

    Service.prototype.save = function (saleOrder) {
        var action = saleOrder.id ? '/sale-orders/update' : '/sale-orders/create';

        return this.$http.post(action, {
            id: saleOrder.id,
            storeId: saleOrder.store.id,
            issueDate: saleOrder.issueDate,
            additionalInformation: saleOrder.additionalInformation
        });
    };

    angular.module('dyoub.app').service('SaleOrder', [
        '$http',
        Service
    ]);

})();
