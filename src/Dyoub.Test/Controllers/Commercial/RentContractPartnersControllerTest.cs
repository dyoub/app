// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.RentContractPartners;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContractPartners;
using Dyoub.Test.Contexts.Commercial.RentContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class RentContractPartnersControllerTest
    {
        [TestMethod]
        public async Task ListRentContractPartners()
        {
            ListRentContractPartnersContext context = new ListRentContractPartnersContext();
            RentContractPartnersController controller = new RentContractPartnersController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            RentContractPartnerListJson result = (RentContractPartnerListJson)await controller.List(viewModel);

            Assert.IsTrue(result.RentContract.RentContractPartners.Count() == context.RentContractPartners.Count());
        }

        [TestMethod]
        public async Task UpdateRentContractPartners()
        {
            UpdateRentContractPartnersContext context = new UpdateRentContractPartnersContext();
            RentContractPartnersController controller = new RentContractPartnersController(context);

            UpdateRentContractPartnerListViewModel viewModel = new UpdateRentContractPartnerListViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.Partners.Add(context.AnotherPartner.Id);

            await controller.Update(viewModel);

            Assert.IsTrue(context.RentContractPartnersHasBeenUpdated());
        }
    }
}
