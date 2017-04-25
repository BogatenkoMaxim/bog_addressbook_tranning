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
            ContactData newContact = new ContactData("Klavdian", "Cepkins");

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.ChekingContract();
            app.Contacts.ConModify(0, newContact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = newContact.Firstname;
            oldContacts[0].LastName = newContact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
