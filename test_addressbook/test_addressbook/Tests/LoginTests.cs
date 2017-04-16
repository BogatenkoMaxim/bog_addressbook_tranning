using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithtValidCredentials()
        {
            app.Auth.LogOut();
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInValidCredentials()
        {
            app.Auth.LogOut();
            AccountData account = new AccountData("admin", "qwerty");
            app.Auth.Login(account);
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
