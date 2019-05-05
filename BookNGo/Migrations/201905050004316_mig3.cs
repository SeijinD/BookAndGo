namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "HouseId", "dbo.Houses");
            DropIndex("dbo.Reservations", new[] { "HouseId" });
            AddColumn("dbo.Reservations", "House_HouseId", c => c.Int());
            AlterColumn("dbo.Reservations", "HouseId", c => c.String());
            CreateIndex("dbo.Reservations", "House_HouseId");
            AddForeignKey("dbo.Reservations", "House_HouseId", "dbo.Houses", "HouseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "House_HouseId", "dbo.Houses");
            DropIndex("dbo.Reservations", new[] { "House_HouseId" });
            AlterColumn("dbo.Reservations", "HouseId", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "House_HouseId");
            CreateIndex("dbo.Reservations", "HouseId");
            AddForeignKey("dbo.Reservations", "HouseId", "dbo.Houses", "HouseId", cascadeDelete: true);
        }
    }
}
