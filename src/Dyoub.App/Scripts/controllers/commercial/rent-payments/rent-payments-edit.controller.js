// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(handleError, path, PaymentMethod, RentPayments) {
        this.handleError = handleError;
        this.path = path;
        this.PaymentMethod = PaymentMethod;
        this.RentPayments = RentPayments;
    }

    Controller.prototype.addPayment = function () {
        var controller = this,
            totalPayable = controller.rentContract.totalPayable,
            totalPaid = controller.rentContract.totalPaid,
            paymentList = controller.rentContract.paymentList;

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
        controller.rentContract.totalPaid = 0;

        if (controller.rentContract.paymentList) {
            controller.rentContract.paymentList.forEach(function (payment) {
                controller.rentContract.totalPaid += (payment.total || 0);
            });
        }
    };

    Controller.prototype.calculateTotalPayable = function () {
        var controller = this,
            discount = controller.rentContract.discount || 0,
            totalPayable = controller.rentContract.total - (controller.rentContract.total * discount / 100);

        controller.rentContract.totalPayable = totalPayable.round(2);
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.paymentToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = controller.path
            .map('/rent-contracts/edit/:rentContractId/items');

        controller.searchPayments();
        controller.searchPaymentMethods();
    };

    Controller.prototype.noPayments = function () {
        var controller = this;
        return !controller.searchingPayments &&
            controller.rentContract.paymentList &&
            controller.rentContract.paymentList.isEmpty();
    };

    Controller.prototype.paidOut = function () {
        var controller = this;
        return controller.rentContract &&
            controller.rentContract.totalPaid === controller.rentContract.totalPayable;
    };

    Controller.prototype.removePayment = function () {
        var controller = this,
            payment = controller.paymentToRemove,
            paymentIndex = controller.rentContract.paymentList.indexOf(payment);

        controller.rentContract.totalPaid -= payment.total;
        controller.rentContract.paymentList.splice(paymentIndex, 1);
        controller.paymentToRemove = null;
    };

    Controller.prototype.saleHasItems = function () {
        var controller = this;
        return controller.rentContract &&
            controller.rentContract.total > 0;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.RentPayments.save({
            rentContractId: controller.routeParams.rentContractId,
            discount: controller.rentContract.discount,
            payments: controller.rentContract.paymentList
        })
        .then(function () {
            controller.path.redirectTo('/rent-contracts/details/:rentContractId',
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

        controller.RentPayments
            .list(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;

                if (controller.rentContract) {
                    controller.calculateTotalPayable();
                    controller.calculateTotalPaid();
                    controller.rentContract.paymentList.forEach(function (payment) {
                        controller.calculateInstallmentOptions(payment);
                    });
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

    angular.module('dyoub.app').controller('RentPaymentsEditController', [
        'HandleError',
        'Path',
        'PaymentMethod',
        'RentPayments',
        Controller
    ]);

})();
