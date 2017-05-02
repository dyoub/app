// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PaymentMethod, SalePayments) {
        this.handleError = handleError;
        this.path = path;
        this.PaymentMethod = PaymentMethod;
        this.SalePayments = SalePayments;
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

    Controller.prototype.calculateInstallmentOptions = function (payment) {
        var controller = this,
            total = payment.total,
            installmentLimit = payment.method ? payment.method.installmentLimit : 0,
            numberOfInstallments = Math.min(payment.numberOfInstallments || 1, installmentLimit);

        payment.installmentOptions = [];

        for (var installmentNumber = 1; installmentNumber <= installmentLimit; installmentNumber++) {
            payment.installmentOptions.push({
                numberOfInstallments: installmentNumber,
                installmentValue: total ? (total / installmentNumber).round(2) : null
            });
        }

        payment.installmentOption = payment.installmentOptions
            .where(function (installmentOption) {
                return installmentOption.numberOfInstallments === numberOfInstallments;
            })
            .first();
    };

    Controller.prototype.calculatePayment = function (payment) {
        var controller = this;
        controller.calculateTotalPaid();
        controller.calculateInstallmentOptions(payment);
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
            totalPayable = controller.total - (controller.total * discount / 100);

        controller.totalPayable = totalPayable.round(2);
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.paymentToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/sale-orders/edit/:saleOrderId/items');

        controller.searchPayments();
        controller.searchPaymentMethods();
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

    Controller.prototype.saleHasItems = function () {
        var controller = this;
        return controller.total > 0;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.SalePayments.save({
            saleOrderId: controller.routeParams.saleOrderId,
            discount: controller.discount,
            payments: controller.paymentList
        })
        .then(function () {
            controller.path.redirectTo('/sale-orders/details/:saleOrderId',
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

        controller.SalePayments
            .list(controller.routeParams.saleOrderId)
            .then(function (response) {
                controller.total = response.data.total;
                controller.discount = response.data.discount;
                controller.totalPayable = response.data.totalPayable;
                controller.confirmed = response.data.confirmed;
                controller.paymentList = response.data.paymentList;
                controller.calculateTotalPayable();
                controller.calculateTotalPaid();
                controller.paymentList.forEach(function (payment) {
                    controller.calculateInstallmentOptions(payment);
                });
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

    Controller.prototype.searchPaymentMethods = function () {
        var controller = this;
        controller.searchingPaymentMethods = true;
        controller.PaymentMethod
            .listActives()
            .then(function (response) {
                controller.paymentMethodList = response.data;
            })
            ['catch'](function (response) {
                controller.handleError(response);
            })
            ['finally'](function () {
                controller.searchingPaymentMethods = false;
            });
    };

    Controller.prototype.showRemovePaymentDialog = function (payment) {
        var controller = this;
        controller.paymentToRemove = payment;
    };

    angular.module('dyoub.app').controller('SalePaymentsEditController', [
        'HandleError',
        'Path',
        'PaymentMethod',
        'SalePayments',
        Controller
    ]);

})();
