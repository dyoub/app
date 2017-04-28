// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Financial;
using Dyoub.App.Models.ViewModel.Financial.Wallets;
using Dyoub.App.Results.Financial.Wallets;
using Dyoub.Test.Contexts.Financial.Wallets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Financial
{
    [TestClass]
    public class WalletsControllerTest
    {
        [TestMethod]
        public async Task CreateWallet()
        {
            CreateWalletContext context = new CreateWalletContext();
            WalletsController controller = new WalletsController(context);

            CreateWalletViewModel viewModel = new CreateWalletViewModel();
            viewModel.Name = "Test";

            await controller.Create(viewModel);

            Assert.IsTrue(context.WalletWasCreated());
        }

        [TestMethod]
        public async Task DeleteWallet()
        {
            DeleteWalletContext context = new DeleteWalletContext();
            WalletsController controller = new WalletsController(context);

            WalletIdViewModel viewModel = new WalletIdViewModel();
            viewModel.Id = context.Wallet.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.WalletWasDeleted());
        }

        [TestMethod]
        public async Task FindWallet()
        {
            FindWalletContext context = new FindWalletContext();
            WalletsController controller = new WalletsController(context);

            WalletIdViewModel viewModel = new WalletIdViewModel();
            viewModel.Id = context.Wallet.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is WalletJson);
        }

        [TestMethod]
        public async Task ListWallet()
        {
            ListWalletContext context = new ListWalletContext();
            WalletsController controller = new WalletsController(context);

            WalletListJson result = (WalletListJson)await controller.List(new ListWalletsViewModel());

            Assert.IsTrue(result.Wallets.Count() == context.Wallets.Count());
        }

        [TestMethod]
        public async Task UpdateWallet()
        {
            UpdateWalletContext context = new UpdateWalletContext();
            WalletsController controller = new WalletsController(context);

            UpdateWalletViewModel viewModel = new UpdateWalletViewModel();
            viewModel.Id = context.Wallet.Id;
            viewModel.Name = context.Wallet.Name + " Updated";
            viewModel.AdditionalInformation = context.Wallet.AdditionalInformation + " Updated";
            viewModel.Active = !context.Wallet.Active;

            await controller.Update(viewModel);

            Assert.IsTrue(context.WalletWasUpdated());
        }
    }
}
