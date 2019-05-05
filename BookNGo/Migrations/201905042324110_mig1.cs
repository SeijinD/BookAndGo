namespace BookNGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.AvailabilityId)
                .ForeignKey("dbo.Houses", t => t.HouseId_HouseId)
                .Index(t => t.HouseId_HouseId);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        HouseId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        MaxOccupancy = c.Int(nullable: false),
                        PricePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocationId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HouseId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.LocationId)
                .Index(t => t.CategoryId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.FeatureId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        HouseID_HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Houses", t => t.HouseID_HouseId, cascadeDelete: true)
                .Index(t => t.HouseID_HouseId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NumberOfOccupants = c.Int(nullable: false),
                        DateOfBooking = c.DateTime(nullable: false),
                        Comments = c.String(),
                        PriceCharged = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApplicationUserId = c.Int(nullable: false),
                        HouseId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Houses", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        Gender = c.String(maxLength: 255),
                        DateOfBirth = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Location_LocationId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.FeatureHouses",
                c => new
                    {
                        Feature_FeatureId = c.Int(nullable: false),
                        House_HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Feature_FeatureId, t.House_HouseId })
                .ForeignKey("dbo.Features", t => t.Feature_FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Houses", t => t.House_HouseId, cascadeDelete: true)
                .Index(t => t.Feature_FeatureId)
                .Index(t => t.House_HouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservations", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Houses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Availabilities", "HouseId_HouseId", "dbo.Houses");
            DropForeignKey("dbo.Houses", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Images", "HouseID_HouseId", "dbo.Houses");
            DropForeignKey("dbo.FeatureHouses", "House_HouseId", "dbo.Houses");
            DropForeignKey("dbo.FeatureHouses", "Feature_FeatureId", "dbo.Features");
            DropForeignKey("dbo.Houses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.FeatureHouses", new[] { "House_HouseId" });
            DropIndex("dbo.FeatureHouses", new[] { "Feature_FeatureId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Location_LocationId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reservations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reservations", new[] { "HouseId" });
            DropIndex("dbo.Images", new[] { "HouseID_HouseId" });
            DropIndex("dbo.Houses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Houses", new[] { "CategoryId" });
            DropIndex("dbo.Houses", new[] { "LocationId" });
            DropIndex("dbo.Availabilities", new[] { "HouseId_HouseId" });
            DropTable("dbo.FeatureHouses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reservations");
            DropTable("dbo.Locations");
            DropTable("dbo.Images");
            DropTable("dbo.Features");
            DropTable("dbo.Categories");
            DropTable("dbo.Houses");
            DropTable("dbo.Availabilities");
        }
    }
}
