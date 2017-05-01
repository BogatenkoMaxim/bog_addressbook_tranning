using System;
using System.Text;
using System.Collections.Generic;
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

        // Выполняемые действия над контактами
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper ConCreate(ContactData contact)
        {
            InitNewContact();
            FillContractForm(contact);
            SubmitNewContract();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ConModify(int index, ContactData newContact)
        {
            SelectContact(index);
            EditNewContract();
            FillContractForm(newContact);
            SubmitNewContractModify();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ConRemove(int index)
        {
            SelectContact(index);
            RemoveContact();
            ReturnToHomePage();
            return this;
        }

// Методы манипуляции с контактами
        public ContactHelper EditNewContract()
        {
            driver.FindElement(By.XPath("(//img[@title='Edit'])[ 1 ]")).Click();
            return this;
        }

        public ContactHelper SubmitNewContractModify()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }


        public ContactHelper SubmitNewContract()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
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
            contactCache = null;
            return this;
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

// Метода проверки наличия контактов в HomePage
        public void ChekingContract()
        {
            if (IsContractIn() != true)
            {
                ContactData forModify = new ContactData("Maxim", "Bogatenko");
                manager.Contacts.ConCreate(forModify);
            }
        }

        private bool IsContractIn()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

// Кэширование
        private List<ContactData> contactCache = null;

// Методы сравнения групп
        public List<ContactData> GetContactList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    IWebElement lastName = element.FindElements(By.TagName("td"))[1];
                    IWebElement firstName = element.FindElements(By.TagName("td"))[2];

                    contactCache.Add(new ContactData(lastName.Text, firstName.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("Value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

//Работа с текстом
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

// Чтение информации с таблицы или формы
        internal ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(lastName, firstName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        internal ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            NewInitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("Value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("Value");
            //string address = driver.FindElement(By.Name("address")).GetAttribute("Value");
            string address = driver.FindElement(By.Name("address")).Text;
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("Value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("Value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("Value");


            return new ContactData(lastName, firstName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
            };
        }

        public void NewInitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

    }
}
