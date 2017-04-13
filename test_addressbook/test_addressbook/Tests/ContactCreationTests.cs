using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Maxim", "Bogatenko");

            app.Contacts.ConCreate(contact);
            app.Auth.ReturnAndLogout();
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.ConCreate(contact);
            app.Auth.ReturnAndLogout();
        }
    }
}
