// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.ServiceModel.Account.Users;
using Dyoub.Test.Contexts.Account.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Dyoub.Test.Models.ServiceModel.Account
{
    [TestClass]
    public class UserSignupTest
    {
        [TestMethod]
        public async Task ShouldNotSignupWithEmailAlreadyTaken()
        {
            SignupEmailAlreadyTakenContext context = new SignupEmailAlreadyTakenContext();

            UserSignup userSignup = new UserSignup(context, context.User);
            await userSignup.SignupAsync();

            Assert.IsTrue(userSignup.EmailAlreadyTaken);
            Assert.IsTrue(context.HasOnlyOneUserWithThisEmail());
        }

        [TestMethod]
        public async Task ShouldCreateUserDataAfterSignup()
        {
            SignupCorrectlyContext context = new SignupCorrectlyContext();

            UserSignup userSignup = new UserSignup(context, context.User);
            await userSignup.SignupAsync();

            Assert.IsTrue(context.TheUserWasCreated());
            Assert.IsTrue(context.ATenantWasCreatedForTheUser());
            Assert.IsTrue(context.AClosureRequestWasCreatedForTheUser());
        }
    }
}
