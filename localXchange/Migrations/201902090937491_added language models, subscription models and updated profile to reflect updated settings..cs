namespace localXchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlanguagemodelssubscriptionmodelsandupdatedprofiletoreflectupdatedsettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        typeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.langContents",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        langId = c.Int(nullable: false),
                        langContentTitle = c.String(),
                        langContentValue = c.String(),
                        language_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.languageNationalities", t => t.language_id)
                .Index(t => t.language_id);
            
            CreateTable(
                "dbo.languageNationalities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        language = c.String(),
                        lanAbbr = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.langLabels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        langId = c.Int(nullable: false),
                        langDisplayTextTitle = c.String(),
                        langDisplayTextValue = c.String(),
                        language_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.languageNationalities", t => t.language_id)
                .Index(t => t.language_id);
            
            CreateTable(
                "dbo.subscriptionPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        subscriptionLevel = c.String(),
                        abbreviatedName = c.String(),
                        listPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.userProfileModels", "accountTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.userProfileModels", "languageId", c => c.Int(nullable: false));
            AddColumn("dbo.userProfileModels", "languageNationalities_id", c => c.Int());
            CreateIndex("dbo.userProfileModels", "accountTypeId");
            CreateIndex("dbo.userProfileModels", "languageNationalities_id");
            AddForeignKey("dbo.userProfileModels", "accountTypeId", "dbo.accountTypes", "Id", cascadeDelete: false);
            AddForeignKey("dbo.userProfileModels", "languageNationalities_id", "dbo.languageNationalities", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.userProfileModels", "languageNationalities_id", "dbo.languageNationalities");
            DropForeignKey("dbo.userProfileModels", "accountTypeId", "dbo.accountTypes");
            DropForeignKey("dbo.langLabels", "language_id", "dbo.languageNationalities");
            DropForeignKey("dbo.langContents", "language_id", "dbo.languageNationalities");
            DropIndex("dbo.userProfileModels", new[] { "languageNationalities_id" });
            DropIndex("dbo.userProfileModels", new[] { "accountTypeId" });
            DropIndex("dbo.langLabels", new[] { "language_id" });
            DropIndex("dbo.langContents", new[] { "language_id" });
            DropColumn("dbo.userProfileModels", "languageNationalities_id");
            DropColumn("dbo.userProfileModels", "languageId");
            DropColumn("dbo.userProfileModels", "accountTypeId");
            DropTable("dbo.subscriptionPlans");
            DropTable("dbo.langLabels");
            DropTable("dbo.languageNationalities");
            DropTable("dbo.langContents");
            DropTable("dbo.accountTypes");
        }
    }
}
