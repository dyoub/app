// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {
            marketed: true,
            isManufactured: false,
            stockMovement: false,
            canFraction: false
        };
    };

    Service.prototype.find = function (productId) {
        return this.$http.post('/products/find', { id: productId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/products', filter);
    };

    Service.prototype.remove = function (productId) {
        return this.$http.post('/products/delete', { id: productId });
    };

    Service.prototype.save = function (product) {
        var action = product.id ? '/products/update' : '/products/create';

        return this.$http.post(action, {
            id: product.id,
            name: product.name,
            code: product.code,
            unitOfMeasure: product.unitOfMeasure,
            additionalInformation: product.additionalInformation,
            marketed: product.marketed,
            isManufactured: product.isManufactured,
            stockMovement: product.stockMovement,
            canFraction: product.canFraction
        });
    };

    angular.module('dyoub.app').service('Product', [
        '$http',
        Service
    ]);

})();
