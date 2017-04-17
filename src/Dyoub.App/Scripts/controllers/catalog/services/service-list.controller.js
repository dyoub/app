// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(Service, handleError) {
        this.Service = Service;
        this.handleError = handleError;
    }

    Controller.prototype.clearFilter = function () {
        var controller = this;
        controller.filter = {};
    };

    Controller.prototype.init = function () {
        var controller = this;
        controller.clearFilter();
        controller.newSearch();
    };

    Controller.prototype.newSearch = function () {
        var controller = this;
        controller.filter.index = 0;
        controller.serviceList = [];
        controller.search();
    };

    Controller.prototype.search = function () {
        var controller = this;
        controller.searching = true;

        controller.Service
            .list(controller.filter)
            .then(function (response) {
                controller.serviceList.pushRange(response.data);
                controller.filter.index = controller.serviceList.length;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    angular.module('dyoub.app').controller('ServiceListController', [
        'Service',
        'HandleError',
        Controller
    ]);

})();
