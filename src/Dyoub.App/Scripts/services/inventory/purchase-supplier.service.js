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

    Service.prototype.saveOrder = function (purchaseSupplier) {
        return this.$http.post('/purchase-orders/supplier/update', {
            purchaseOrderId: purchaseSupplier.purchaseOrderId,
            supplierId: purchaseSupplier.supplierId
        });
    };

    Service.prototype.save = function (purchaseSupplier) {
        var service = this;

        if (purchaseSupplier.supplier) {
            return this.Supplier.save(purchaseSupplier.supplier).then(function (response) {
                purchaseSupplier.supplierId = response.data.id;
                return service.saveOrder(purchaseSupplier);
            });
        } else {
            return service.saveOrder(purchaseSupplier);
        }
    };

    angular.module('dyoub.app').service('PurchaseSupplier', [
        '$http',
        'Supplier',
        Service
    ]);

})();
