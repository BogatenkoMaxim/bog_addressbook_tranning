﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData(string lastname, string firstname)
        {
            Firstname = firstname;
            LastName = lastname;
        }

        public string Firstname { get; set; }

        public string LastName { get; set; }

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
            return Firstname + LastName;
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

        public string Id { get; set; }
    }
}
