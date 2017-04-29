// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (saleOrderId) {
        return this.$http.post('/sale-orders/items', { id: saleOrderId });
    };

    Service.prototype.save = function (saleItems) {
        return this.$http.post('/sale-orders/items/update', {
            saleOrderId: saleItems.saleOrderId,
            items: saleItems.items.select(function (item) {
                return {
                    id: item.id,
                    isProduct: item.isProduct,
                    isService: item.isService,
                    discount: item.discount,
                    quantity: item.quantity
                }
            })
        });
    };

    angular.module('dyoub.app').service('SaleItems', [
        '$http',
        Service
    ]);

})();
