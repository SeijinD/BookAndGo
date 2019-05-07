namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FeatureHouses", newName: "HouseFeatures");
            DropForeignKey("dbo.Availabilities", "HouseId_HouseId", "dbo.Houses");
            DropIndex("dbo.Availabilities", new[] { "HouseId_HouseId" });
            DropPrimaryKey("dbo.HouseFeatures");
            AddPrimaryKey("dbo.HouseFeatures", new[] { "House_HouseId", "Feature_FeatureId" });
            DropTable("dbo.Availabilities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        AvailabilityId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        HouseId_HouseId = c.Int(),
                    })
                .PrimaryKey(t => t.AvailabilityId);
            
            DropPrimaryKey("dbo.HouseFeatures");
            AddPrimaryKey("dbo.HouseFeatures", new[] { "Feature_FeatureId", "House_HouseId" });
            CreateIndex("dbo.Availabilities", "HouseId_HouseId");
            AddForeignKey("dbo.Availabilities", "HouseId_HouseId", "dbo.Houses", "HouseId");
            RenameTable(name: "dbo.HouseFeatures", newName: "FeatureHouses");
        }
    }
}
