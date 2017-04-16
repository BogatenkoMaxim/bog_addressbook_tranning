using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private bool acceptNextAlert = true;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper ConCreate(ContactData contact)
        {
            manager.Navigator.GoToHomePage();

            InitNewContact();
            FillContractForm(contact);
            SubmitNewContract();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ConModify(int index, ContactData newContact)
        {
            manager.Navigator.GoToHomePage();

            if (IsContractIn() != true)
            {
                ContactData forModify = new ContactData("Maxim", "Bogatenko");
                manager.Contacts.ConCreate(forModify);
            }

            SelectContact(index);
            EditNewContract(1);
            FillContractForm(newContact);
            SubmitNewContractModify();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ConRemove(int index)
        {
            manager.Navigator.GoToHomePage();

            if (IsContractIn() != true)
            {
                ContactData forModify = new ContactData("Maxim", "Bogatenko");
                manager.Contacts.ConCreate(forModify);
            }

            SelectContact(index);
            RemoveContact();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper EditNewContract(int index)
        {
            driver.FindElement(By.XPath("(//img[@title='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SubmitNewContractModify()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }


        public ContactHelper SubmitNewContract()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper FillContractForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), "Nikolaevich");
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), "MaxBog");
            Type(By.Name("title"), "Yes");
            Type(By.Name("company"), "Yes");
            Type(By.Name("address"), "Yes");
            Type(By.Name("home"), "Yes");
            Type(By.Name("mobile"), "Yes");
            Type(By.Name("work"), "Yes");
            Type(By.Name("fax"), "Yes");
            Type(By.Name("email"), "Yes");
            Type(By.Name("email2"), "Yes");
            Type(By.Name("email3"), "Yes");
            Type(By.Name("homepage"), "Yes");
            TypeCon(By.Name("bday"), "2");
            TypeCon(By.Name("bmonth"), "November");
            Type(By.Name("byear"), "1992");
            TypeCon(By.Name("aday"), "2");
            TypeCon(By.Name("amonth"), "November");
            Type(By.Name("ayear"), "1992");
            Type(By.Name("address2"), "Yes");
            Type(By.Name("phone2"), "Yes");
            Type(By.Name("notes"), "Yes");
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper InitNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            return this;
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

        private bool IsContractIn()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
