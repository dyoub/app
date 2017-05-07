// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http, Supplier) {
        this.$http = $http;
        this.Supplier = Supplier;
    }

    Service.prototype.find = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/supplier', { id: purchaseOrderId });
    };

    Service.prototype.save = function (purchaseSupplier) {
        var service = this;

        return this.Supplier
            .save(purchaseSupplier.supplier)
            .then(function (response) {
                return service.$http.post('/purchase-orders/supplier/update', {
                    purchaseOrderId: purchaseSupplier.purchaseOrderId,
                    supplierId: response.data.id
                });
            });
    };

    angular.module('dyoub.app').service('PurchaseSupplier', [
        '$http',
        'Supplier',
        Service
    ]);

})();
