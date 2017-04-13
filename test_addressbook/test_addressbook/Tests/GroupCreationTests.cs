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
            GroupData group = new GroupData("group1");
            group.Header = "group1";
            group.Footer = "group1";

            app.Groups.Create(group);
            app.Auth.ReturnToGroupPage();
        }

        [Test]
        public void EmptyGroupCreationTests()
        {
            app.Navigator.GoToGroupPage(); GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.Create(group);
            app.Auth.ReturnToGroupPage();
        }
    }
}
