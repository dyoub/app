// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Profile) {
        this.handleError = handleError;
        this.path = path;
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

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Profile
                .save(controller.user)
                .then(function () {
                    controller.path.redirectTo('/profile');
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                    controller.saving = false;
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.profileForm.$invalid
    };

    Controller.prototype.searchComplete = function () {
        var controller = this;
        return !(controller.searching || controller.error);
    };

    angular.module('dyoub.app').controller('ProfileEditController', [
        'HandleError',
        'Path',
        'Profile',
        Controller
    ]);

})();
