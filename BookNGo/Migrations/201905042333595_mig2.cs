namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Houses", name: "ApplicationUser_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.Houses", name: "IX_ApplicationUser_Id", newName: "IX_Owner_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Houses", name: "IX_Owner_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Houses", name: "Owner_Id", newName: "ApplicationUser_Id");
        }
    }
}
