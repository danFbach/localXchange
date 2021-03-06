﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace localXchange.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private const string db_name = "localXchange_db";

        public ApplicationDbContext()
            : base(db_name, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<unitsModel> unitsModel { get; set; }
        public DbSet<userProfileModel> userprofilemodel { get; set; }
        public DbSet<productModel> productmodel { get; set; }
        public DbSet<productCategory> productCategory { get; set; }
        public DbSet<ratingModel> ratingmodel { get; set; }
        public DbSet<productImage> productImage { get; set; }
        public DbSet<messagingModel> messagingModel { get; set; }
        public DbSet<addressBookModel> addressBookModel { get; set; }
        public DbSet<accountType> accountType { get; set; }
        public DbSet<subscriptionPlan> subscriptionPlans { get; set; }
        public DbSet<languageNationalities> languages { get; set; }
        public DbSet<langLabels> langLabel { get; set; }
        public DbSet<langContent> langContent { get; set; }
    }
}