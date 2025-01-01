namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Bookings", "Email", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Bookings", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String());
            AlterColumn("dbo.Bookings", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Bookings", "Email", c => c.String());
            AlterColumn("dbo.Bookings", "Name", c => c.String());
        }
    }
}
