using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactModifyTests : AuthTestBase
    {

        [Test]
        public void ContactModifyTest()
        {
            ContactData newContact = new ContactData("Maxis", "Bogatenkos");

            app.Navigator.GoToHomePage();
            app.Contacts.ChekingContract();
            app.Contacts.ConModify(1, newContact);
        }

    }
}
