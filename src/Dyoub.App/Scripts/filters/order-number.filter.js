// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    function OrderNumberFilter() {
        return function (id) {
            if (!id) return;

            var orderNumber = id.toString();

            if (orderNumber.length < 6) {
                orderNumber = ('000000' + orderNumber).slice(-6);
            }

            return orderNumber;
        }
    }

    angular.module('dyoub.app').filter('orderNumber', OrderNumberFilter);
})();
