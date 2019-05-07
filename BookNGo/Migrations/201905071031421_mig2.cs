namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Houses", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.Houses", name: "IX_Owner_Id", newName: "IX_OwnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Houses", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.Houses", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
