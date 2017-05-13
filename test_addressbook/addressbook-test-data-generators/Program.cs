using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];
            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10)));
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            if (format == "csv")
            {
                WriteGroupsToCsvFile(groups, writer);
            }
            else if (format == "xml")
            {
                switch (args[1])
                {
                    case "groups.xml":
                        WriteGroupsToXmlFile(groups, writer);
                        break;

                    case "contacts.xml":
                        WriteContactsToXmlFile(contacts, writer);
                        break;

                    default :
                        System.Console.Out.Write("Not find" + writer.ToString());
                        break;
                }
            }
            else if (format == "json")
            {
                switch (args[1])
                {
                    case "groups.json":
                        WriteGroupsToJsonFile(groups, writer);
                        break;

                    case "contacts.json":
                        WriteContactsToJsonFile(contacts, writer);
                        break;

                    default:
                        System.Console.Out.Write("Not find" + writer.ToString());
                        break;
                }
            }
            else
            {
                System.Console.Out.Write("Unrecognized format" + format);
            }

            writer.Close();
        }

 // Создание файла Csv, Xml и Json для групп
        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

// Создание файла Xml и Json для групп
        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

    }
}
