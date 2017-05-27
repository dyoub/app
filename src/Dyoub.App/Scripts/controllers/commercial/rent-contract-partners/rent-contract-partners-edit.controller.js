// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {

    function Controller(dialog, handleError, path, Partner, RentContractPartners) {
        this.dialog = dialog;
        this.handleError = handleError;
        this.path = path;
        this.Partner = Partner;
        this.RentContractPartners = RentContractPartners;
    }

    Controller.prototype.addPartner = function (partner) {
        var controller = this;

        var alreadyAdded = controller.rentContract.partnerList.where(function (selectedPartner) {
            return selectedPartner.id === partner.id;
        })
        .any();

        if (alreadyAdded) {
            controller.dialog.error('Partner already added.');
        } else {
            controller.rentContract.partnerList.push(partner);
        }
    };

    Controller.prototype.addFirstPartner = function () {
        var controller = this;

        if (controller.partners.any() && controller.partnerSuggestionOpened) {
            var firstPartner = controller.partners.first();
            controller.addPartner(firstPartner);
        }
    };

    Controller.prototype.hideRemoveDialog = function () {
        var controller = this;
        controller.partnerToRemove = null;
    };

    Controller.prototype.init = function () {
        var controller = this;

        controller.routeParams = this.path
            .map('/rent-contracts/edit/:rentContractId/partners');

        controller.searchSelectedPartners();
    };

    Controller.prototype.newPartnerSearch = function () {
        var controller = this;
        controller.partnerSuggestionOpened = true;
        controller.searchingPartners = true;
        controller.partners = [];
    };

    Controller.prototype.noPartners = function () {
        var controller = this;
        return controller.partners &&
            controller.partners.isEmpty() &&
            !controller.searchingPartners;
    };

    Controller.prototype.noPartnersSelected = function () {
        var controller = this;
        return controller.rentContract &&
            controller.rentContract.partnerList &&
            controller.rentContract.partnerList.isEmpty() &&
            !controller.searchingSelectedPartners;
    };

    Controller.prototype.goToNextStep = function () {
        var controller = this,
            path = controller.rentContract.confirmed ?
                '/rent-contracts/details/:rentContractId/partners' :
                '/rent-contracts/edit/:rentContractId/customer';
    
        controller.path.redirectTo(path, controller.routeParams);
    };

    Controller.prototype.removePartner = function () {
        var controller = this,
            partnerIndex = controller.rentContract.partnerList
                .indexOf(controller.partnerToRemove);

        controller.rentContract.partnerList.splice(partnerIndex, 1);
        controller.partnerToRemove = null;
    };

    Controller.prototype.save = function () {
        var controller = this;

        if (controller.saveBlocked() || controller.saving) {
            return;
        }

        controller.saving = true;

        controller.RentContractPartners.save({
            rentContractId: controller.routeParams.rentContractId,
            partners: controller.rentContract.partnerList
        })
        .then(function () {
            controller.goToNextStep();
        })
        ['catch'](function (response) {
            controller.handleError(response);
            controller.saving = false;
        });
    };

    Controller.prototype.saveBlocked = function () {
        var controller = this;
        return controller.saving ||
               controller.rentContractPartnersForm.$invalid ||
               !controller.rentContract ||
               !controller.rentContract.partnerList;
    };

    Controller.prototype.searchPartners = function () {
        var controller = this;
        controller.searchingPartners = true;

        controller.Partner.list({
            name: controller.partnerSearch.name,
            limit: 3
        })
        .then(function (response) {
            controller.partners = response.data;
        })
        ['catch'](function (response) {
            controller.handleError(response);
        })
        ['finally'](function () {
            controller.searchingPartners = false;
        });
    };

    Controller.prototype.searchSelectedPartners = function () {
        var controller = this;
        controller.searchingSelectedPartners = true;

        controller.RentContractPartners
            .list(controller.routeParams.rentContractId)
            .then(function (response) {
                controller.rentContract = response.data;
                controller.notFound = !controller.rentContract;
            })
            ['catch'](function (response) {
                controller.handleError(response);
                controller.error = true;
            })
            ['finally'](function () {
                controller.searchingSelectedPartners = false;
            });
    };

    Controller.prototype.searchSelectedPartnersComplete = function () {
        var controller = this;
        return !(controller.searchingSelectedPartners || controller.error);
    };

    Controller.prototype.showRemovePartnerDialog = function (partner) {
        var controller = this;
        controller.partnerToRemove = partner;
    };

    angular.module('dyoub.app').controller('RentContractPartnersEditController', [
        'Dialog',
        'HandleError',
        'Path',
        'Partner',
        'RentContractPartners',
        Controller
    ]);

})();
