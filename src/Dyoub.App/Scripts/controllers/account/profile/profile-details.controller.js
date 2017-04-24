// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, Profile) {
        this.handleError = handleError;
        this.Profile = Profile;
    }

    Controller.prototype.init = function () {
        var controller = this;
        controller.searching = true;

        controller.Profile
            .find()
            .then(function (response) {
                controller.user = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searching = false;
            });
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('ProfileDetailsController', [
        'HandleError',
        'Profile',
        Controller
    ]);

})();
