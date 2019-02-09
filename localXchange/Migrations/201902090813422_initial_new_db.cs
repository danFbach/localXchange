namespace localXchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_new_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.addressBookModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        bookOwnerUserID = c.String(maxLength: 128),
                        bookEntryUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.bookEntryUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.bookOwnerUserID)
                .Index(t => t.bookOwnerUserID)
                .Index(t => t.bookEntryUserID);
            
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
                "dbo.messagingModels",
                c => new
                    {
                        messageID = c.Int(nullable: false, identity: true),
                        senderID = c.String(nullable: false, maxLength: 128),
                        receiverID = c.String(nullable: false, maxLength: 128),
                        subject = c.String(maxLength: 150),
                        message = c.String(nullable: false, maxLength: 2500),
                        datetimeSent = c.DateTime(nullable: false),
                        important = c.Boolean(nullable: false),
                        readUnread = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.messageID)
                .ForeignKey("dbo.AspNetUsers", t => t.receiverID, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.senderID, cascadeDelete: false)
                .Index(t => t.senderID)
                .Index(t => t.receiverID);
            
            CreateTable(
                "dbo.productCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        categoryName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.productImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fileName = c.String(),
                        relativePath = c.String(),
                        productID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.productModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        dateListed = c.DateTime(nullable: false),
                        sellerID = c.String(nullable: false),
                        productName = c.String(nullable: false),
                        images = c.Boolean(nullable: false),
                        categoryID = c.Int(nullable: false),
                        unitID = c.Int(nullable: false),
                        unitQTY = c.Decimal(precision: 18, scale: 2),
                        qtyAvail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        qtyRemain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        state = c.String(nullable: false),
                        city = c.String(nullable: false, maxLength: 50),
                        zipcode = c.Int(nullable: false),
                        address = c.String(maxLength: 50),
                        productCategory_ID = c.Int(),
                        sellerModel_Id = c.String(maxLength: 128),
                        unitModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.productCategories", t => t.productCategory_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.sellerModel_Id)
                .ForeignKey("dbo.unitsModels", t => t.unitModel_ID)
                .Index(t => t.productCategory_ID)
                .Index(t => t.sellerModel_Id)
                .Index(t => t.unitModel_ID);
            
            CreateTable(
                "dbo.ratingModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userID = c.String(nullable: false, maxLength: 128),
                        productID = c.String(nullable: false),
                        rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        description = c.String(),
                        product_ID = c.Int(),
                        userProfileModel_ID = c.String(maxLength: 128),
                        userProfileModel_ID1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.productModels", t => t.product_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.userID, cascadeDelete: false)
                .ForeignKey("dbo.userProfileModels", t => t.userProfileModel_ID)
                .ForeignKey("dbo.userProfileModels", t => t.userProfileModel_ID1)
                .Index(t => t.userID)
                .Index(t => t.product_ID)
                .Index(t => t.userProfileModel_ID)
                .Index(t => t.userProfileModel_ID1);
            
            CreateTable(
                "dbo.unitsModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        unitName = c.String(),
                        unitAbvr = c.String(),
                        unitType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.userProfileModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        fName = c.String(nullable: false, maxLength: 25),
                        MInitial = c.String(maxLength: 1),
                        lName = c.String(nullable: false, maxLength: 35),
                        state = c.String(nullable: false),
                        city = c.String(nullable: false, maxLength: 50),
                        zipcode = c.Int(nullable: false),
                        address = c.String(maxLength: 50),
                        phoneNumber = c.String(),
                        contactEmail = c.String(),
                        messagesToEmail = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.userProfileModels", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ratingModels", "userProfileModel_ID1", "dbo.userProfileModels");
            DropForeignKey("dbo.ratingModels", "userProfileModel_ID", "dbo.userProfileModels");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.productModels", "unitModel_ID", "dbo.unitsModels");
            DropForeignKey("dbo.productModels", "sellerModel_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ratingModels", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ratingModels", "product_ID", "dbo.productModels");
            DropForeignKey("dbo.productModels", "productCategory_ID", "dbo.productCategories");
            DropForeignKey("dbo.messagingModels", "senderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.messagingModels", "receiverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.addressBookModels", "bookOwnerUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.addressBookModels", "bookEntryUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.userProfileModels", new[] { "UserID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ratingModels", new[] { "userProfileModel_ID1" });
            DropIndex("dbo.ratingModels", new[] { "userProfileModel_ID" });
            DropIndex("dbo.ratingModels", new[] { "product_ID" });
            DropIndex("dbo.ratingModels", new[] { "userID" });
            DropIndex("dbo.productModels", new[] { "unitModel_ID" });
            DropIndex("dbo.productModels", new[] { "sellerModel_Id" });
            DropIndex("dbo.productModels", new[] { "productCategory_ID" });
            DropIndex("dbo.messagingModels", new[] { "receiverID" });
            DropIndex("dbo.messagingModels", new[] { "senderID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.addressBookModels", new[] { "bookEntryUserID" });
            DropIndex("dbo.addressBookModels", new[] { "bookOwnerUserID" });
            DropTable("dbo.userProfileModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.unitsModels");
            DropTable("dbo.ratingModels");
            DropTable("dbo.productModels");
            DropTable("dbo.productImages");
            DropTable("dbo.productCategories");
            DropTable("dbo.messagingModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.addressBookModels");
        }
    }
}
