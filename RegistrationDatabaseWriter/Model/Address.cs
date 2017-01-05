using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationDatabaseWriter.Model
{
    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public string Precinct { get; set; }

        public string Ward { get; set; }
    }

}
