namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactInfoesaddOpenDay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactInfoes", "OpenDay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactInfoes", "OpenDay");
        }
    }
}
