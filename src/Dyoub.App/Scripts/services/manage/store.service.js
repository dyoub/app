// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service($http) {
        this.$http = $http;
    }

    Service.prototype.create = function () {
        return { active: true };
    };

    Service.prototype.find = function (storeId) {
        return this.$http.post('/stores/find', { id: storeId });
    };

    Service.prototype.list = function (filter) {
        return this.$http.post('/stores', filter);
    };

    Service.prototype.remove = function (storeId) {
        return this.$http.post('/stores/delete', { id: storeId });
    };

    Service.prototype.save = function (store) {
        return this.$http.post(store.id ? '/stores/update' : '/stores/create', {
            id: store.id,
            name: store.name,
            active: store.active
        });
    };

    angular.module('dyoub.app').service('Store', [
        '$http',
        Service
    ]);

})();
