namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Houses", "Availability_AvailabilityId", "dbo.Availabilities");
            DropIndex("dbo.Houses", new[] { "Availability_AvailabilityId" });
            AddColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Features", "FeatureName", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Reservations", "NumberOfOccupants", c => c.Int(nullable: false));
            DropColumn("dbo.Houses", "Availability_AvailabilityId");
            DropColumn("dbo.Categories", "NameCategory");
            DropColumn("dbo.Features", "NameFeature");
            DropColumn("dbo.Reservations", "OccupantsNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "OccupantsNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Features", "NameFeature", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Categories", "NameCategory", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Houses", "Availability_AvailabilityId", c => c.Int());
            DropColumn("dbo.Reservations", "NumberOfOccupants");
            DropColumn("dbo.Features", "FeatureName");
            DropColumn("dbo.Categories", "CategoryName");
            CreateIndex("dbo.Houses", "Availability_AvailabilityId");
            AddForeignKey("dbo.Houses", "Availability_AvailabilityId", "dbo.Availabilities", "AvailabilityId");
        }
    }
}
