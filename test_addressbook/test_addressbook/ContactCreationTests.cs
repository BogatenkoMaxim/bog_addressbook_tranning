﻿using System;
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
            navigationHelper.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactHelper.InitNewContact();
            contactHelper.FillContractForm(new ContactData("Maxim", "Bogatenko"));
            contactHelper.SubmitNewContract();
            loginHelper.ReturnAndLogout();
        }
    }
}
