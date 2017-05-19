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
            totalPayable = controller.purchaseOrder.totalPayable,
            totalPaid = controller.purchaseOrder.totalPaid,
            paymentList = controller.purchaseOrder.paymentList;

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
        controller.purchaseOrder.totalPaid = 0;

        if (controller.purchaseOrder.paymentList) {
            controller.purchaseOrder.paymentList.forEach(function (payment) {
                controller.purchaseOrder.totalPaid += (payment.total || 0);
            });
        }
    };

    Controller.prototype.calculateTotalPayable = function () {
        var controller = this,
            discount = controller.purchaseOrder.discount || 0,
            shippingCost = controller.purchaseOrder.shippingCost || 0,
            otherTaxes = controller.purchaseOrder.otherTaxes || 0,
            total = controller.purchaseOrder.total,
            totalPayable = total + shippingCost + otherTaxes
                - (controller.purchaseOrder.total * discount / 100);

        controller.purchaseOrder.totalPayable = totalPayable.round(2);
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
            controller.purchaseOrder.paymentList &&
            controller.purchaseOrder.paymentList.isEmpty();
    };

    Controller.prototype.paidOut = function () {
        var controller = this;
        return controller.purchaseOrder &&
            controller.purchaseOrder.totalPaid === controller.purchaseOrder.totalPayable;
    };

    Controller.prototype.removePayment = function () {
        var controller = this,
            payment = controller.paymentToRemove,
            paymentIndex = controller.purchaseOrder.paymentList.indexOf(payment);

        controller.purchaseOrder.totalPaid -= payment.total;
        controller.purchaseOrder.paymentList.splice(paymentIndex, 1);
        controller.paymentToRemove = null;
    };

    Controller.prototype.purchaseHasProducts = function () {
        var controller = this;
        return controller.purchaseOrder &&
            controller.purchaseOrder.total > 0;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.PurchasePayments.save({
            purchaseOrderId: controller.routeParams.purchaseOrderId,
            shippingCost: controller.purchaseOrder.shippingCost,
            otherTaxes: controller.purchaseOrder.otherTaxes,
            discount: controller.purchaseOrder.discount,
            payments: controller.purchaseOrder.paymentList
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
                controller.purchaseOrder = response.data;
                controller.notFound = !controller.purchaseOrder;

                if (controller.purchaseOrder) {
                    controller.calculateTotalPayable();
                    controller.calculateTotalPaid();
                }
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
