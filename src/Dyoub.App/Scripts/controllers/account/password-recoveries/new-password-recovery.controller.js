// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller($http, dialog, handleError) {
        this.$http = $http;
        this.dialog = dialog;
        this.handleError = handleError;
    }

    Controller.prototype.sendEmail = function () {
        var controller = this;

        if (controller.resetPasswordForm.$invalid) {
            controller.dialog.error('Make sure the fields are filled correctly.');
            return;
        }

        controller.sending = true;

        controller.$http.post('/reset-password/confirmation', {
            email: controller.email,
        })
        .then(function () {
            controller.sent = true;
        })
        ['catch'](function (response) {
            controller.handleError(response);
            controller.sending = false;
        });
    };

    angular.module('dyoub.app').controller('NewPasswordRecoveryController', [
        '$http',
        'Dialog',
        'HandleError',
        Controller
    ]);

})();
