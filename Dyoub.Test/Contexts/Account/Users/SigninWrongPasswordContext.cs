using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.Users
{
    public class SigninWrongPasswordContext : InMemoryContext
    {
        public User User { get; private set; }

        public SigninWrongPasswordContext()
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));
            SaveChanges();
        }

        public bool UserIsNotSiginedIn()
        {
            Entry(User).Reload();
            return User.Token == null;
        }
    }
}
