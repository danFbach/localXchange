namespace localXchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1_initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.aProductModels", newName: "productModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.productModels", newName: "aProductModels");
        }
    }
}
