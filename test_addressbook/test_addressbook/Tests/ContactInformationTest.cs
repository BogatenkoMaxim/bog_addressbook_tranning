﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactInformationTest : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestSerch()
        {
            System.Console.Out.Write(app.Contacts.GetNumberOfResults());
        }

        [Test]
        public void TestContactInformationDetails()
        {
            string fromEdit = app.Contacts.GetContactInformationFromEditFormFull(0);
            string fromDetails = app.Contacts.GetContactInformationFromDetails(0);

            Assert.AreEqual(fromEdit, fromDetails);
        }
    }
}
