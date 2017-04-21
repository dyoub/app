// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    function InstallmentConditionFilter(translate) {
        return function (installmentNumber) {
            return translate('From') + ' ' + installmentNumber + 'x';
        }
    }

    angular.module('dyoub.app').filter('installmentCondition', [
        'Translate',
        InstallmentConditionFilter
    ]);
})();
