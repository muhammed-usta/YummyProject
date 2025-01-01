namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migeditbookingmessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "MessageContent", c => c.String());
            DropColumn("dbo.Bookings", "MessageCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "MessageCount", c => c.String());
            DropColumn("dbo.Bookings", "MessageContent");
        }
    }
}
