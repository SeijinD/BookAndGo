namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "LocationId", "dbo.Locations");
            DropIndex("dbo.AspNetUsers", new[] { "LocationId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "LocationId", newName: "Location_LocationId");
            AlterColumn("dbo.AspNetUsers", "Location_LocationId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Location_LocationId");
            AddForeignKey("dbo.AspNetUsers", "Location_LocationId", "dbo.Locations", "LocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Location_LocationId", "dbo.Locations");
            DropIndex("dbo.AspNetUsers", new[] { "Location_LocationId" });
            AlterColumn("dbo.AspNetUsers", "Location_LocationId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.AspNetUsers", name: "Location_LocationId", newName: "LocationId");
            CreateIndex("dbo.AspNetUsers", "LocationId");
            AddForeignKey("dbo.AspNetUsers", "LocationId", "dbo.Locations", "LocationId", cascadeDelete: true);
        }
    }
}
