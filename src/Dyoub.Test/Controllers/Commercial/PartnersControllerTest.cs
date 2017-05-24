// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.Partners;
using Dyoub.App.Results.Commercial.Partners;
using Dyoub.Test.Contexts.Commercial.Partners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class PartnersControllerTest
    {
        [TestMethod]
        public async Task CreatePartner()
        {
            CreatePartnerContext context = new CreatePartnerContext();
            PartnersController controller = new PartnersController(context);

            CreatePartnerViewModel viewModel = new CreatePartnerViewModel();
            viewModel.Name = "Partner Test";

            await controller.Create(viewModel);

            Assert.IsTrue(context.PartnerWasCreated());
        }

        [TestMethod]
        public async Task DeletePartner()
        {
            DeletePartnerContext context = new DeletePartnerContext();
            PartnersController controller = new PartnersController(context);

            PartnerIdViewModel viewModel = new PartnerIdViewModel();
            viewModel.Id = context.Partner.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.PartnerWasDeleted());
        }

        [TestMethod]
        public async Task FindPartner()
        {
            FindPartnerContext context = new FindPartnerContext();
            PartnersController controller = new PartnersController(context);

            PartnerIdViewModel viewModel = new PartnerIdViewModel();
            viewModel.Id = context.Partner.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is PartnerJson);
        }

        [TestMethod]
        public async Task ListPartner()
        {
            ListPartnerContext context = new ListPartnerContext();
            PartnersController controller = new PartnersController(context);

            PartnerListJson result = (PartnerListJson)await controller.List(new ListPartnersViewModel());

            Assert.IsTrue(result.Partners.Count() == context.Partners.Count());
        }

        [TestMethod]
        public async Task UpdatePartner()
        {
            UpdatePartnerContext context = new UpdatePartnerContext();
            PartnersController controller = new PartnersController(context);

            UpdatePartnerViewModel viewModel = new UpdatePartnerViewModel();
            viewModel.Id = context.Partner.Id;
            viewModel.Name = context.Partner.Name + " Updated";
            viewModel.NationalId = context.Partner.NationalId + " Updated";
            viewModel.Email = context.Partner.Email + " Updated";
            viewModel.PhoneNumber = context.Partner.PhoneNumber + " Updated";
            viewModel.AlternativePhoneNumber = context.Partner.AlternativePhoneNumber + " Updated";
            viewModel.AdditionalInformation = context.Partner.AdditionalInformation + " Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.PartnerWasUpdated());
        }
    }
}
