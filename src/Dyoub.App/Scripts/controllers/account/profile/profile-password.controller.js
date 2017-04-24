// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, Profile) {
        this.handleError = handleError;
        this.path = path;
        this.Profile = Profile;
    }

    Controller.prototype.save = function () {
        var controller = this;

        if (!controller.saveBlocked()) {
            controller.saving = true;

            controller.Profile
                .changePassword(controller.user)
                .then(function () {
                    controller.path.redirectTo('/profile');
                })
                ['catch'](function (response) {
                    controller.handleError(response);
                });
        }
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving || controller.profileForm.$invalid
    };

    angular.module('dyoub.app').controller('ProfilePasswordController', [
        'HandleError',
        'Path',
        'Profile',
        Controller
    ]);

})();
