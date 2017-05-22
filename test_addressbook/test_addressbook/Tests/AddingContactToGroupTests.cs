using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.Contacts.ChekingContract();
            app.Groups.ChekingGroup();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            
            app.Contacts.ChekingRelation(oldList);

            List<ContactData> fullList = ContactData.GetAll();
            for (int i = 0; i < oldList.Count; i++)
            {
                    fullList.Remove(oldList[i]);
            } 
            ContactData contact = fullList.First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
