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
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroupPage();
            app.Groups.InitNewGroup();
            GroupData group = new GroupData("group1");
            group.Header = "group1";
            group.Footer = "group1";
            app.Groups.FillGroupForm(group);
            app.Groups.SubmitGroupCreation();
            app.Auth.ReturnToGroupPage();
        }
    }
}
