// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return {
            marketed: true,
            canFraction: false
        };
    };

    Service.prototype.find = function (serviceId) {
        return this.$http.post('/services/find', { id: serviceId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/services', filter);
    };

    Service.prototype.remove = function (serviceId) {
        return this.$http.post('/services/delete', { id: serviceId });
    };

    Service.prototype.save = function (service) {
        var action = service.id ? '/services/update' : '/services/create';

        return this.$http.post(action, {
            id: service.id,
            name: service.name,
            code: service.code,
            unitOfMeasure: service.unitOfMeasure,
            additionalInformation: service.additionalInformation,
            marketed: service.marketed,
            canFraction: service.canFraction
        });
    };

    angular.module('dyoub.app').service('Service', [
        '$http',
        Service
    ]);

})();
