// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    function InstallmentOptionFilter($filter, translate) {
        var currencyFilter = $filter('currency');

        return function (installmentOption) {
            var optionDescription = installmentOption.numberOfInstallments + 'x ';

            if (!installmentOption.installmentValue) {
                return optionDescription;
            }

            return optionDescription +
                translate('of') + ' ' + currencyFilter(installmentOption.installmentValue);
        }
    }

    angular.module('dyoub.app').filter('installmentOption', [
        '$filter',
        'Translate',
        InstallmentOptionFilter
    ]);
})();
