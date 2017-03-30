// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller($http, path, dialog, handleError) {
        this.$http = $http;
        this.path = path;
        this.dialog = dialog;
        this.handleError = handleError;
    }

    Controller.prototype.signin = function () {
        var controller = this;

        if (controller.signinForm.$invalid) {
            controller.dialog.error('Make sure the fields are filled correctly.');
            return;
        }

        controller.working = true;

        controller.$http.post('/signin', {
            email: controller.user.email,
            password: controller.user.password
        })
        .then(function () {
            controller.path.redirectTo('/dashboard');
        })
        ['catch'](function (response) {
            controller.handleError(response);
            controller.working = false;
        });
    };

    angular.module('dyoub.app').controller('SigninController', [
        '$http',
        'Path',
        'Dialog',
        'HandleError',
        Controller
    ]);

})();
