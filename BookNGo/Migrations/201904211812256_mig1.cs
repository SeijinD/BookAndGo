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
                        Picture = c.Binary(),
                        Category_CategoryId = c.Int(),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.HouseId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Location_LocationId);
            
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
                        House_HouseId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Houses", t => t.House_HouseId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.House_HouseId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        UserName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        HouseId_HouseId = c.Int(),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Houses", t => t.HouseId_HouseId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.HouseId_HouseId)
                .Index(t => t.Location_LocationId);
            
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

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
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
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

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


        }

        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Users", "HouseId_HouseId", "dbo.Houses");
            DropForeignKey("dbo.Reservations", "House_HouseId", "dbo.Houses");
            DropForeignKey("dbo.Availabilities", "HouseId_HouseId", "dbo.Houses");
            DropForeignKey("dbo.Houses", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.FeatureHouses", "House_HouseId", "dbo.Houses");
            DropForeignKey("dbo.FeatureHouses", "Feature_FeatureId", "dbo.Features");
            DropForeignKey("dbo.Houses", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.FeatureHouses", new[] { "House_HouseId" });
            DropIndex("dbo.FeatureHouses", new[] { "Feature_FeatureId" });
            DropIndex("dbo.Users", new[] { "Location_LocationId" });
            DropIndex("dbo.Users", new[] { "HouseId_HouseId" });
            DropIndex("dbo.Reservations", new[] { "User_UserId" });
            DropIndex("dbo.Reservations", new[] { "House_HouseId" });
            DropIndex("dbo.Houses", new[] { "Location_LocationId" });
            DropIndex("dbo.Houses", new[] { "Category_CategoryId" });
            DropIndex("dbo.Availabilities", new[] { "HouseId_HouseId" });
            DropTable("dbo.FeatureHouses");
            DropTable("dbo.Users");
            DropTable("dbo.Reservations");
            DropTable("dbo.Locations");
            DropTable("dbo.Features");
            DropTable("dbo.Categories");
            DropTable("dbo.Houses");
            DropTable("dbo.Availabilities");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
