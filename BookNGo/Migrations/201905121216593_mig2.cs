namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        HouseId_HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Houses", t => t.HouseId_HouseId, cascadeDelete: true)
                .Index(t => t.HouseId_HouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "HouseId_HouseId", "dbo.Houses");
            DropIndex("dbo.Images", new[] { "HouseId_HouseId" });
            DropTable("dbo.Images");
        }
    }
}
