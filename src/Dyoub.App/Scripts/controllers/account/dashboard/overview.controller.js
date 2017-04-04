// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller($http, handleError) {
        this.$http = $http;
        this.handleError = handleError;
    }

    Controller.prototype.load = function () {
        var controller = this;

        controller.$http.post('/dashboard/load')
            .then(function (response) {
                controller.overview = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            });
    };

    angular.module('dyoub.app').controller('OverviewController', [
        '$http',
        'HandleError',
        Controller
    ]);

})();
