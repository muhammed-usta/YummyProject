namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        AboutId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ImageUrl2 = c.String(),
                        Item1 = c.String(),
                        Item2 = c.String(),
                        Item3 = c.String(),
                        Description = c.String(),
                        VideoUrl = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.AboutId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        PersonCount = c.Int(nullable: false),
                        MessageCount = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ProductName = c.String(),
                        Ingredients = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Chefs",
                c => new
                    {
                        ChefId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Name = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ChefId);
            
            CreateTable(
                "dbo.ChefSocials",
                c => new
                    {
                        ChefSocialId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Icon = c.String(),
                        SocialMediaName = c.String(),
                        ChefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChefSocialId)
                .ForeignKey("dbo.Chefs", t => t.ChefId, cascadeDelete: true)
                .Index(t => t.ChefId);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ContactInfoId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        MapUrl = c.String(),
                        OpenHours = c.String(),
                    })
                .PrimaryKey(t => t.ContactInfoId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        VideoUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FeatureId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Subject = c.String(),
                        MessageContent = c.String(),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID);
            
            CreateTable(
                "dbo.PhotoGalleries",
                c => new
                    {
                        PhotoGalleryID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.PhotoGalleryID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        TestimonialId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        NameSurname = c.String(),
                        Title = c.String(),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestimonialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChefSocials", "ChefId", "dbo.Chefs");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ChefSocials", new[] { "ChefId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Testimonials");
            DropTable("dbo.Services");
            DropTable("dbo.PhotoGalleries");
            DropTable("dbo.Messages");
            DropTable("dbo.Features");
            DropTable("dbo.Events");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.ChefSocials");
            DropTable("dbo.Chefs");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Bookings");
            DropTable("dbo.Admins");
            DropTable("dbo.Abouts");
        }
    }
}
