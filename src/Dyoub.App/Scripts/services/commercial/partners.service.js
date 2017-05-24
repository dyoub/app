// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {};
    };

    Service.prototype.find = function (partnerId) {
        return this.$http.post('/partners/find', { id: partnerId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/partners', filter);
    };

    Service.prototype.remove = function (partnerId) {
        return this.$http.post('/partners/delete', { id: partnerId });
    };

    Service.prototype.save = function (partner) {
        var action = partner.id ? '/partners/update' : '/partners/create';

        return this.$http.post(action, {
            id: partner.id,
            name: partner.name,
            nationalId: partner.nationalId,
            email: partner.email,
            phoneNumber: partner.phoneNumber,
            alternativePhoneNumber: partner.alternativePhoneNumber,
            additionalInformation: partner.additionalInformation
        });
    };

    angular.module('dyoub.app').service('Partner', [
        '$http',
        Service
    ]);

})();
