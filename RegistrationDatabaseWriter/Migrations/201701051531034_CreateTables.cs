namespace RegistrationDatabaseWriter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Line1 = c.String(),
                        Line2 = c.String(),
                        Line3 = c.String(),
                        Precinct = c.String(),
                        Ward = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        RegistrationDate = c.DateTime(),
                        Affiliation = c.String(),
                        Unit = c.String(),
                        Status1504 = c.String(),
                        Status1411 = c.String(),
                        Status1407 = c.String(),
                        Status1404 = c.String(),
                        Status1304 = c.String(),
                        Status1211 = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "Address_Id", "dbo.Address");
            DropIndex("dbo.Person", new[] { "Address_Id" });
            DropTable("dbo.Person");
            DropTable("dbo.Address");
        }
    }
}
