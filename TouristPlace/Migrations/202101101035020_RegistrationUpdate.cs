namespace TouristPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistrationUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TouristRegistrations", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TouristRegistrations", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TouristRegistrations", "Email", c => c.String());
            AlterColumn("dbo.TouristRegistrations", "Name", c => c.String(nullable: false));
        }
    }
}
