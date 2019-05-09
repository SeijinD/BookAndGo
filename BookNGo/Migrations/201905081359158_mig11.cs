namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "HouseID_HouseId", "dbo.Houses");
            DropIndex("dbo.Images", new[] { "HouseID_HouseId" });
            AddColumn("dbo.Houses", "ImageUrl", c => c.String(nullable: false));
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        HouseID_HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Houses", "ImageUrl");
            CreateIndex("dbo.Images", "HouseID_HouseId");
            AddForeignKey("dbo.Images", "HouseID_HouseId", "dbo.Houses", "HouseId", cascadeDelete: true);
        }
    }
}
