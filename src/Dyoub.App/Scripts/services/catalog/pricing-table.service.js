// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.clear = function (storeId) {
        return this.$http.post('/pricing-tables/clear', { storeId: storeId });
    };

    Service.prototype.copy = function (fromStoreId, toStoreId) {
        return this.$http.post('/pricing-tables/copy', {
            storeId: toStoreId,
            sourceStoreId: fromStoreId
        });
    };

    Service.prototype.find = function (storeId) {
        return this.$http.post('/pricing-tables/find', { storeId: storeId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/pricing-tables', filter);
    };

    Service.prototype.listItems = function (filter) {
        return this.$http.post('/pricing-tables/items', filter);
    };

    Service.prototype.listItemsForSale = function (filter) {
        return this.$http.post('/pricing-tables/items/for-sale', filter);
    };

    Service.prototype.save = function (storeId, items) {
        return this.$http.post('/pricing-tables/update', {
            storeId: storeId,
            items: items.select(function (item) {
                return {
                    id: item.id,
                    isProduct: item.isProduct,
                    isService: item.isService,
                    unitRentPrice: item.unitRentPrice,
                    unitSalePrice: item.unitSalePrice
                };
            })
        });
    };

    angular.module('dyoub.app').service('PricingTable', [
        '$http',
        Service
    ]);

})();
