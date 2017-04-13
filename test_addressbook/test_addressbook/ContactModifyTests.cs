﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactModifyTests : TestBase
    {

        [Test]
        public void ContactModifyTest()
        {
            ContactData newContact = new ContactData("Maxis", "Bogatenkos");

            app.Contacts.ConModify(1, newContact);
            app.Auth.ReturnAndLogout();
        }

    }
}
