using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class DeleteContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestDeleteContactToGroup()
        {
            app.Groups.ChekingGroup();
            app.Contacts.ChekingContract();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().First();
            if (oldList.Count == 0)
            {
                app.Contacts.AddContactToGroup(contact, group);
            }
            else
            {
                contact = oldList.First();
            }

            app.Contacts.DeleteContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}