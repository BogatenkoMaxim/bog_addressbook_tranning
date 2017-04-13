using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("group2");
            newData.Header = "group2";
            newData.Footer = "group2";

            app.Groups.Modify(1, newData);
            app.Auth.ReturnToGroupPage();
        }
    }
}
