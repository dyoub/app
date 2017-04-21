// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    function InstallmentLimitOptionFilter(translate) {
        return function (installmentLimit) {
            return translate('Until') + ' ' + installmentLimit + 'x';
        }
    }

    angular.module('dyoub.app').filter('installmentLimitOption', [
        'Translate',
        InstallmentLimitOptionFilter
    ]);
})();
