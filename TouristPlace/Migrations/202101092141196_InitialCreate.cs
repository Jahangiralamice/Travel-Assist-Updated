namespace TouristPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToristPlaces",
                c => new
                    {
                        ToristPlaceId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Description = c.String(nullable: false),
                        TouristRegistrationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToristPlaceId)
                .ForeignKey("dbo.TouristRegistrations", t => t.TouristRegistrationId, cascadeDelete: true)
                .Index(t => t.TouristRegistrationId);
            
            CreateTable(
                "dbo.TouristRegistrations",
                c => new
                    {
                        TouristRegistrationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        PhoneNo = c.String(),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TouristRegistrationId);
            
            CreateTable(
                "dbo.TouristComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        ToristPlaceId = c.Int(nullable: false),
                        TouristRegistrationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToristPlaces", "TouristRegistrationId", "dbo.TouristRegistrations");
            DropIndex("dbo.ToristPlaces", new[] { "TouristRegistrationId" });
            DropTable("dbo.TouristComments");
            DropTable("dbo.TouristRegistrations");
            DropTable("dbo.ToristPlaces");
        }
    }
}
