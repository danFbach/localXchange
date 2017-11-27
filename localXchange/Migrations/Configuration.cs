namespace localXchange.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

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
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "StdUser" }
            //);
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
