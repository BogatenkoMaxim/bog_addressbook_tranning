using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]

    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string deprecated;

        public ContactData()
        {

        }


        public ContactData(string lastname, string firstname)
        {
            Firstname = firstname;
            LastName = lastname;
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated
        {
            get
            {
                return deprecated;
            }
            set
            {
                deprecated = value;
            }
        }
   
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

//Очистка
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

// Метода сравнения и сортировки
        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return (Firstname+LastName).GetHashCode();
        }

        public override string ToString()
        {
            return "Имя = " + Firstname + "\nФамилия = " + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (LastName.Equals(other.LastName))
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return LastName.CompareTo(other.LastName);
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {

                List<ContactData> oldContacts = (from c in db.Contacts select c).ToList();
                List<ContactData> trustContactList = new List<ContactData>();
                foreach (ContactData con in oldContacts)
                {
                    if (con.Deprecated == DateTime.MinValue.ToString())
                    {
                        trustContactList.Add(con);
                    }
                }
                return trustContactList;

            }
        }

        public static List<ContactData> GetAll2()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00")
                        select c).ToList();
            }
        }
    }
}
