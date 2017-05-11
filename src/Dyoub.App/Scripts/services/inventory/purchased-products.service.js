// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (purchaseOrderId) {
        return this.$http.post('/purchase-orders/products', { id: purchaseOrderId });
    };

    Service.prototype.save = function (purchasedProducts) {
        return this.$http.post('/purchase-orders/products/update', {
            purchaseOrderId: purchasedProducts.purchaseOrderId,
            products: purchasedProducts.products.select(function (product) {
                return {
                    id: product.id,
                    unitCost: product.unitCost,
                    quantity: product.quantity,
                    discount: product.discount,
                }
            })
        });
    };

    angular.module('dyoub.app').service('PurchasedProducts', [
        '$http',
        Service
    ]);

})();
