// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Service() { }

    Service.prototype.create = function (max) {
        var installments = [];

        for (var installment = 1; installment <= max; installment++) {
            installments.push(installment);
        }

        return installments;
    };

    angular.module('dyoub.app').service('Installment', [
        Service
    ]);

})();
