// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.list = function (rentContractId) {
        return this.$http.post('/rent-contracts/partners', { id: rentContractId });
    };

    Service.prototype.save = function (rentContractPartners) {
        return this.$http.post('/rent-contracts/partners/update', {
            rentContractId: rentContractPartners.rentContractId,
            partners: rentContractPartners.partners.select(function (partner) {
                return partner.id;
            })
        });
    };

    angular.module('dyoub.app').service('RentContractPartners', [
        '$http',
        Service
    ]);

})();
