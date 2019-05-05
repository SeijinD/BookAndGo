namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "House_HouseId", "dbo.Houses");
            DropIndex("dbo.Reservations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reservations", new[] { "House_HouseId" });
            DropColumn("dbo.Reservations", "ApplicationUserId");
            DropColumn("dbo.Reservations", "HouseId");
            RenameColumn(table: "dbo.Reservations", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Reservations", name: "House_HouseId", newName: "HouseId");
            AlterColumn("dbo.Reservations", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Reservations", "HouseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "HouseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "ApplicationUserId");
            CreateIndex("dbo.Reservations", "HouseId");
            AddForeignKey("dbo.Reservations", "HouseId", "dbo.Houses", "HouseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "HouseId", "dbo.Houses");
            DropIndex("dbo.Reservations", new[] { "HouseId" });
            DropIndex("dbo.Reservations", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Reservations", "HouseId", c => c.Int());
            AlterColumn("dbo.Reservations", "HouseId", c => c.String());
            AlterColumn("dbo.Reservations", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Reservations", name: "HouseId", newName: "House_HouseId");
            RenameColumn(table: "dbo.Reservations", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Reservations", "HouseId", c => c.String());
            AddColumn("dbo.Reservations", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "House_HouseId");
            CreateIndex("dbo.Reservations", "ApplicationUser_Id");
            AddForeignKey("dbo.Reservations", "House_HouseId", "dbo.Houses", "HouseId");
        }
    }
}
