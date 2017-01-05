using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationDatabaseWriter.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using RegistrationDatabaseWriter.Model;

    class RegistrationContext : DbContext
    {
        public RegistrationContext() : base("RegistrationContext")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
