using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Linq;


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

        public ContactHelper ConModify(ContactData contact, ContactData newContact)
        {
            SelectContact(contact.Id);
            EditNewContract(contact.Id);
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

        public ContactHelper ConRemove(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveContact();
            ReturnToHomePage();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void DeleteContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            ClearGroupFilter();
            SelectGroupFilter();
            SelectContact(contact.Id);
            CommitDeleteContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
               .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

// Методы манипуляции с контактами
        public ContactHelper EditNewContract()
        {
            driver.FindElement(By.XPath("(//img[@title='Edit'])[ 1 ]")).Click();
            return this;
        }

        public ContactHelper EditNewContract(string id)
        {
            ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
            foreach (IWebElement element in elements)
            {
                string p = element.FindElement(By.Name("selected[]"))
                    .GetAttribute("Value");
                 if (p == id)
                {
                    //element.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
                    element.FindElement(By.XPath("(.//img[@title='Edit'])")).Click();
                    break;
                }
            }
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

        private void SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
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

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group")))
                .SelectByText(GroupData.GetAll()[0].Name);
        }

        private void CommitDeleteContactToGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }


// Метода проверки наличия контактов в HomePage
        public void ChekingContract()
        {
            manager.Navigator.GoToHomePage();
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

        public void ChekingRelation(List<ContactData> oldList)
        {
            if (oldList.Count() == ContactData.GetAll2().Count)
            {
                ContactData forModify = new ContactData("Satana", "Astana");
                manager.Contacts.ConCreate(forModify);
            }
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
        public ContactData GetContactInformationFromTable(int index)
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

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            NewInitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("Value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("Value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            //string address = driver.FindElement(By.Name("address")).Text;
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

        public string GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElements(By.Name("entry"))[index].FindElement(By.XPath("//img[@title='Details']")).Click();
            string content = driver.FindElement(By.Id("content")).Text.Replace("\r\n", "").Replace(" ", "");
            return content;
        }

        public string GetContactInformationFromEditFormFull(int index)
        {
            manager.Navigator.GoToHomePage();
            NewInitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("Value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("Value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("Value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("Value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("Value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("Value");
            string address = driver.FindElement(By.Name("address")).Text;

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("Value");
            string trueHomePhone = string.IsNullOrEmpty(homePhone) ? string.Empty : $"H:{homePhone}";

            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("Value");
            string trueMobilePhone = string.IsNullOrEmpty(mobilePhone) ? string.Empty : $"M:{mobilePhone}";

            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("Value");
            string trueWorkPhone = string.IsNullOrEmpty(workPhone) ? string.Empty : $"W:{workPhone}";

            string fax = driver.FindElement(By.Name("fax")).GetAttribute("Value");
            string trueFax = string.IsNullOrEmpty(fax) ? string.Empty : $"F:{fax}";

            string email = driver.FindElement(By.Name("email")).GetAttribute("Value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("Value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("Value");

            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("Value");
            string trueHomePage = string.IsNullOrEmpty(homePage) ? string.Empty : $"Homepage:{homePage}";

            string sumEdit = (firstName + middleName + lastName + nickName + company + title + address + trueHomePhone
                + trueMobilePhone + trueWorkPhone + trueFax + email + email2 + email3 + trueHomePage).Replace(" ", "");
            return sumEdit;
        }

        public void NewInitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

// Число строк в контактах
        public int GetNumberOfResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);

        }

    }
}
