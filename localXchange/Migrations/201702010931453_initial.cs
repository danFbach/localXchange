namespace localXchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        sellerID = c.String(nullable: false),
                        productName = c.String(nullable: false),
                        qtyAvail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        qtyRemain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        country = c.String(nullable: false),
                        state = c.String(nullable: false),
                        city = c.String(nullable: false),
                        zipcode = c.Int(nullable: false),
                        address = c.String(),
                        unitSoldBy = c.Int(nullable: false),
                        qtySoldBy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ratingModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userID = c.String(nullable: false),
                        rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        description = c.String(),
                        ProductModel_ID = c.Int(),
                        userProfileModel_ID = c.String(maxLength: 128),
                        userProfileModel_ID1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.ProductModel_ID)
                .ForeignKey("dbo.userProfileModels", t => t.userProfileModel_ID)
                .ForeignKey("dbo.userProfileModels", t => t.userProfileModel_ID1)
                .Index(t => t.ProductModel_ID)
                .Index(t => t.userProfileModel_ID)
                .Index(t => t.userProfileModel_ID1);
            
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
                "dbo.userProfileModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(),
                        fName = c.String(nullable: false),
                        MInitial = c.String(),
                        lName = c.String(nullable: false),
                        state = c.String(nullable: false),
                        city = c.String(nullable: false),
                        zipcode = c.Int(nullable: false),
                        address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ratingModels", "userProfileModel_ID1", "dbo.userProfileModels");
            DropForeignKey("dbo.ratingModels", "userProfileModel_ID", "dbo.userProfileModels");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ratingModels", "ProductModel_ID", "dbo.ProductModels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ratingModels", new[] { "userProfileModel_ID1" });
            DropIndex("dbo.ratingModels", new[] { "userProfileModel_ID" });
            DropIndex("dbo.ratingModels", new[] { "ProductModel_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.userProfileModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ratingModels");
            DropTable("dbo.ProductModels");
        }
    }
}
