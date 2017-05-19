// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (rentContractId) {
        return this.$http.post('/rent-contracts/products', { id: rentContractId });
    };

    Service.prototype.save = function (rentedProducts) {
        return this.$http.post('/rent-contracts/products/update', {
            rentContractId: rentedProducts.rentContractId,
            products: rentedProducts.products.select(function (product) {
                return {
                    id: product.id,
                    quantity: product.quantity,
                    discount: product.discount,
                }
            })
        });
    };

    angular.module('dyoub.app').service('RentedProducts', [
        '$http',
        Service
    ]);

})();
