// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    function ReceivedDateOptionFilter($filter, translate) {
        var numberFilter = $filter('number');

        return function (daysToReceive) {
            switch (daysToReceive) {
                case null:
                case undefined: return translate('Manually entered');
                case 0: return translate('Same payment date');
                case 1: return daysToReceive + ' ' + translate('day after payment');
                default: return daysToReceive + ' ' + translate('days after payment');
            }
        }
    }

    angular.module('dyoub.app').filter('receivedDateOption', [
        '$filter',
        'Translate',
        ReceivedDateOptionFilter
    ]);
})();
