namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactInfoaddOffDay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactInfoes", "OffDay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactInfoes", "OffDay");
        }
    }
}
