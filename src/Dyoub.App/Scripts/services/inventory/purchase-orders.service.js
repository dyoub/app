// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.confirm = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/confirm', { id: purchaseOrderId });
    };

    Service.prototype.create = function () {
        return { issueDate: new Date() };
    };

    Service.prototype.find = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/find', { id: purchaseOrderId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/purchase-orders', filter);
    };

    Service.prototype.remove = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/delete', { id: purchaseOrderId });
    };

    Service.prototype.revert = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/revert', { id: purchaseOrderId });
    };

    Service.prototype.save = function (purchaseOrder) {
        var action = purchaseOrder.id ? '/purchase-orders/update' : '/purchase-orders/create';

        return this.$http.post(action, {
            id: purchaseOrder.id,
            storeId: purchaseOrder.store.id,
            walletId: purchaseOrder.wallet ? purchaseOrder.wallet.id : null,
            issueDate: purchaseOrder.issueDate,
            additionalInformation: purchaseOrder.additionalInformation
        });
    };

    angular.module('dyoub.app').service('PurchaseOrder', [
        '$http',
        Service
    ]);

})();
