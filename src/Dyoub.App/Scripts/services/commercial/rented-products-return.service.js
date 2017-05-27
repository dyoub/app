// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.save = function (rentContract) {
        return this.$http.post('/rent-contracts/return/update', {
            rentContractId: rentContract.id,
            dateOfReturn: rentContract.dateOfReturn,
            timeOfReturn: rentContract.timeOfReturn,
            products: rentContract.productList.select(function (product) {
                return {
                    id: product.id,
                    returnedQuantity: product.returnedQuantity
                }
            })
        });
    };

    angular.module('dyoub.app').service('RentedProductsReturn', [
        '$http',
        Service
    ]);

})();
