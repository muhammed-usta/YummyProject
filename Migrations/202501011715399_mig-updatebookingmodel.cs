namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migupdatebookingmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Name", c => c.String());
            AlterColumn("dbo.Bookings", "Email", c => c.String());
            AlterColumn("dbo.Bookings", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Bookings", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Bookings", "Email", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Bookings", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
