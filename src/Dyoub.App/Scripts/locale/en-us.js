﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

angular.module("ngLocale", [], ["$provide", function ($provide) {
    var PLURAL_CATEGORY = { ZERO: "zero", ONE: "one", TWO: "two", FEW: "few", MANY: "many", OTHER: "other" };

    function getDecimals(n) {
        n = n + '';
        var i = n.indexOf('.');
        return (i == -1) ? 0 : n.length - i - 1;
    }

    function getVF(n, opt_precision) {
        var v = opt_precision;

        if (undefined === v) {
            v = Math.min(getDecimals(n), 3);
        }

        var base = Math.pow(10, v);
        var f = ((n * base) | 0) % base;
        return { v: v, f: f };
    }

    $provide.value("$locale", {
        DATETIME_FORMATS: {
            MONTH: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            day: "DD",
            weekDay: "dddd",
            month: 'MM',
            monthName: 'MMMM',
            fullDate: "dddd, DD [de] MMMM [de] YYYY",
            longDate: "DD [de] MMMM [de] YYYY",
            medium: "D [de] MMM [de] YY HH:mm:ss",
            mediumDate: "D [de] MMM [de] YY",
            mediumTime: "HH:mm:ss",
            short: "DD/MM/YYYY HH:mm",
            shortDate: "DD/MM/YYYY",
            shortTime: "HH:mm"
        },
        NUMBER_FORMATS: {
            CURRENCY_SYM: "$",
            DECIMAL_SEP: ".",
            GROUP_SEP: ",",
            PATTERNS: [
              { gSize: 3, lgSize: 3, maxFrac: 3, minFrac: 0, minInt: 1, negPre: "-", negSuf: "", posPre: "", posSuf: "" },
              { gSize: 3, lgSize: 3, maxFrac: 2, minFrac: 2, minInt: 1, negPre: "-\u00a4", negSuf: "", posPre: "\u00a4", posSuf: "" }
            ]
        },
        id: "en-us",
        localeID: "en_US",
        pluralCat: function (n, opt_precision) {
            var i = n | 0;
            var vf = getVF(n, opt_precision);
            return (i == 1 && vf.v == 0) ? PLURAL_CATEGORY.ONE : PLURAL_CATEGORY.OTHER;
        },
        translation: {
            'true': 'Yes',
            'false': 'No',
            '0': 'Please check your network connection and try again.',
            '-1': 'Please check your network connection and try again.',
            '401': 'User and/or password is invalid.',
            '403': 'Access denied.',
            '404': 'Could not estabilish communication with the server.',
            '500': 'There was a problem. Please try again.'
        }
    });
}]);
