namespace RegistrationDatabaseWriter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Street", c => c.String());
            AddColumn("dbo.Address", "Zip", c => c.String());
            DropColumn("dbo.Address", "Line1");
            DropColumn("dbo.Address", "Line2");
            DropColumn("dbo.Address", "Line3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Address", "Line3", c => c.String());
            AddColumn("dbo.Address", "Line2", c => c.String());
            AddColumn("dbo.Address", "Line1", c => c.String());
            DropColumn("dbo.Address", "Zip");
            DropColumn("dbo.Address", "Street");
        }
    }
}
