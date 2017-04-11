using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTest : TestBase
    {      
        [Test]
        public void GroupCreationTests()
        {
            navigationHelper.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigationHelper.GoToGroupPage();
            groupHelper.InitNewGroup();
            GroupData group = new GroupData("group1");
            group.Header = "group1";
            group.Footer = "group1";
            groupHelper.FillGroupForm(group);
            groupHelper.SubmitGroupCreation();
            loginHelper.ReturnToGroupPage();
        }
    }
}
