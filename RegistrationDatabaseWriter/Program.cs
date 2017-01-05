using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationDatabaseWriter
{
    using System.Collections;
    using System.Configuration;

    using RegistrationDatabaseWriter.DAL;
    using RegistrationDatabaseWriter.Migrations;
    using RegistrationDatabaseWriter.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var addresses = new Dictionary<string, Address>();
            var lines = new List<string>();

            var context = new RegistrationContext();

            for (var currChar = 'A'; currChar <= 'Z'; currChar++)
            {
                Console.WriteLine($"Reading {currChar} File");
                ReadLines(currChar, lines);
            }

            Console.WriteLine($"{lines.Count} lines found");

            var count = 0;
            foreach (var line in lines)
            {
                count++;
                Console.WriteLine($"Processing line {count}/{lines.Count}");
                // Fields: LastName, FirstName, MiddleInitial, FullAddress, Address1, Address2, Address3, RegistrationDate, Affiliation, Precinct, Ward, Vote0415, Vote1114, Vote0714, Vote0414, Vote0413, Vote1112, SingleMemberDistrict
                var parameters = line.Split(new[] { "," }, StringSplitOptions.None);
                var lastName = parameters[0];
                var firstName = parameters[1];
                var middleInitial = parameters[2];
                var fullAddress = parameters[3];
                var street = parameters[4];
                var unit = parameters[5];
                var zip = parameters[6];
                var registrationDate = parameters[7];
                var affiliation = parameters[8];
                var precinct = parameters[9];
                var ward = parameters[10];
                var vote0415 = parameters[11];
                var vote1114 = parameters[12];
                var vote0714 = parameters[13];
                var vote0414 = parameters[14];
                var vote0413 = parameters[15];
                var vote1211 = parameters[16];
                var singleMemberDistrict = parameters[17];

                var address = FindAddress(street, zip, addresses);
                if (address == null)
                {
                    address = new Address { Street = street, Zip = zip, Precinct = precinct, Ward = ward };
                    addresses.Add(street+zip, address);
                    context.Addresses.Add(address);
                }

                DateTime? regDate;
                DateTime tempDate;
                if (DateTime.TryParse(registrationDate, out tempDate))
                {
                    regDate = tempDate;
                }
                else
                {
                    regDate = null;
                }

                var person = new Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleInitial,
                    Unit = unit,
                    Affiliation = affiliation,
                    RegistrationDate = regDate,
                    Status1211 = vote1211,
                    Status1304 = vote0413,
                    Status1404 = vote0414,
                    Status1407 = vote0714,
                    Status1411 = vote1114,
                    Status1504 = vote0415,
                    Address = address
                };

                context.People.Add(person);

                if (count % 1000 == 0)
                {
                    context.SaveChanges();
                    context.Dispose();
                    context = new RegistrationContext();
                }
            }

            context.SaveChanges();
            context.Dispose();

            Console.WriteLine($"All lines processed");
        }

        private static void ReadLines(char keyChar, List<string> lines)
        {
            var currLines = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["registrationFilePath"] + $"\\registration-{keyChar}.csv").ToList();
            currLines.RemoveAt(0);
            lines.AddRange(currLines);
        }

        private static Address FindAddress(string street, string zip, Dictionary<string, Address> addresses)
        {
            return addresses.ContainsKey(street + zip) ? addresses[street + zip] : null;
        }
    }
}
