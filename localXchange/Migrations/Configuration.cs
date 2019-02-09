namespace localXchange.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<localXchange.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(localXchange.Models.ApplicationDbContext context)
        {
            //context.Roles.AddOrUpdate(x => x.Id,
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Admin" },
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Moderator" },
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "User" }
            //);

            context.accountType.AddOrUpdate(x => x.Id,
                new accountType { typeName = "Administrator" },
                new accountType { typeName = "Moderator" },
                new accountType { typeName = "Employee" },
                new accountType { typeName = "Individual" }
            );

            context.subscriptionPlans.AddOrUpdate(x => x.Id,
                new subscriptionPlan { subscriptionLevel = "Enterprise", abbreviatedName = "ent" },
                new subscriptionPlan { subscriptionLevel = "Small Business", abbreviatedName = "smb" },
                new subscriptionPlan { subscriptionLevel = "Individual", abbreviatedName = "ind" }


            );

            context.languages.AddOrUpdate(x => x.id,
                new languageNationalities { language = "English/US", lanAbbr = "en_US" },
                new languageNationalities { language = "English/UK", lanAbbr = "en_UK" },
                new languageNationalities { language= "Deutsch", lanAbbr="de_DE"}

            );

            //context.unitsModel.AddOrUpdate(
            //    new unitsModel { unitName = "Bunch(s)", unitAbvr = "Bunch", unitType = "Quantity" },
            //    new unitsModel { unitName = "Quantity", unitAbvr = "Qty", unitType = "Quantity" },
            //    new unitsModel { unitName = "Pound(s)", unitAbvr = "lbs", unitType = "Weight" },
            //    new unitsModel { unitName = "Ounce(s)", unitAbvr = "Oz", unitType = "Weight" },
            //    new unitsModel { unitName = "Kilogram(s)", unitAbvr = "kg", unitType = "Weight" },
            //    new unitsModel { unitName = "Gram(s)", unitAbvr = "g", unitType = "Weight" },
            //    new unitsModel { unitName = "Gallon(s)", unitAbvr = "gal", unitType = "Liquid" },
            //    new unitsModel { unitName = "Fluid Ounce(s)", unitAbvr = "FL Oz", unitType = "Liquid" },
            //    new unitsModel { unitName = "Liter(s)", unitAbvr = "L", unitType = "Liquid" },
            //    new unitsModel { unitName = "Mililiter(s)", unitAbvr = "mL", unitType = "Liquid" }
            //);
            //context.productCategory.AddOrUpdate(x => x.ID,
            //    new productCategory { categoryName = "Vegetable" },
            //    new productCategory { categoryName = "Herb" },
            //    new productCategory { categoryName = "Raw Poultry" },
            //    new productCategory { categoryName = "Raw Red Meats" },
            //    new productCategory { categoryName = "Cured Meats" },
            //    new productCategory { categoryName = "Animal Liquid" },
            //    new productCategory { categoryName = "Plant Derived Liquid" }
            //);
        }
    }
}
