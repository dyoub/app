// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PurchasePayments) {
        this.handleError = handleError;
        this.path = path;
        this.PurchasePayments = PurchasePayments;
    }

    Controller.prototype.addPayment = function () {
        var controller = this,
            totalPayable = controller.totalPayable,
            totalPaid = controller.totalPaid,
            paymentList = controller.paymentList;

        paymentList.push({
            date: new Date(),
            total: (totalPayable - totalPaid).round(2)
        });

        controller.calculateTotalPaid();
    };

    Controller.prototype.calculatePayment = function (payment) {
        var controller = this;
        controller.calculateTotalPaid();
    };

    Controller.prototype.calculateTotalPaid = function () {
        var controller = this;
        controller.totalPaid = 0;

        if (controller.paymentList) {
            controller.paymentList.forEach(function (payment) {
                controller.totalPaid += (payment.total || 0);
            });
        }
    };

    Controller.prototype.calculateTotalPayable = function () {
        var controller = this,
            discount = controller.discount || 0,
            shippingCost = controller.shippingCost || 0,
            otherTaxes = controller.otherTaxes || 0,
            totalPayable = controller.total + shippingCost + otherTaxes
                - (controller.total * discount / 100);

        controller.totalPayable = totalPayable.round(2);
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.paymentToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/purchase-orders/edit/:purchaseOrderId/items');

        controller.searchPayments();
    };

    Controller.prototype.noPayments = function () {
        var controller = this;
        return !controller.searchingPayments &&
            controller.paymentList &&
            controller.paymentList.isEmpty();
    };

    Controller.prototype.paidOut = function () {
        var controller = this;
        return controller.totalPaid === controller.totalPayable;
    };

    Controller.prototype.removePayment = function () {
        var controller = this,
            payment = controller.paymentToRemove,
            paymentIndex = controller.paymentList.indexOf(payment);

        controller.totalPaid -= payment.total;
        controller.paymentList.splice(paymentIndex, 1);
        controller.paymentToRemove = null;
    };

    Controller.prototype.purchaseHasProducts = function () {
        var controller = this;
        return controller.total > 0;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.PurchasePayments.save({
            purchaseOrderId: controller.routeParams.purchaseOrderId,
            shippingCost: controller.shippingCost,
            otherTaxes: controller.otherTaxes,
            discount: controller.discount,
            payments: controller.paymentList
        })
        .then(function () {
            controller.path.redirectTo('/purchase-orders/details/:purchaseOrderId',
                controller.routeParams);
        })
        ['catch'](function (response) {
            controller.handleError(response);
            controller.saving = false;
        });
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving ||
            controller.paymentsForm.$invalid ||
            !controller.paidOut();
    };

    Controller.prototype.searchPayments = function () {
        var controller = this;
        controller.searchingPayments = true;

        controller.PurchasePayments
            .list(controller.routeParams.purchaseOrderId)
            .then(function (response) {
                controller.total = response.data.total;
                controller.shippingCost = response.data.shippingCost;
                controller.otherTaxes = response.data.otherTaxes;
                controller.discount = response.data.discount;
                controller.totalPayable = response.data.totalPayable;
                controller.confirmed = response.data.confirmed;
                controller.paymentList = response.data.paymentList;
                controller.calculateTotalPayable();
                controller.calculateTotalPaid();
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingPayments = false;
            });
    };

    Controller.prototype.searchPaymentsComplete = function () {
        var controller = this;
        return !(controller.searchingPayments || controller.error);
    };

    Controller.prototype.showRemovePaymentDialog = function (payment) {
        var controller = this;
        controller.paymentToRemove = payment;
    };

    angular.module('dyoub.app').controller('PurchasePaymentsEditController', [
        'HandleError',
        'Path',
        'PurchasePayments',
        Controller
    ]);

})();
