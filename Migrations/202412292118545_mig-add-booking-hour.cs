namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migaddbookinghour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingHour", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "BookingHour");
        }
    }
}
