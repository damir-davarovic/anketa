namespace Anketa.Migrations
{
    using Anketa.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Anketa.DAL.SurveyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Anketa.DAL.SurveyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var userToSeed = new ApplicationUser
            //{
            //    UserName = "Unidentified User",
            //    PasswordHash = new PasswordHasher().HashPassword("Password123!"),
            //    UserProfileInfo = new UserProfileInfo { Id = 0, userName = "Unidentified User" },
            //    EmailConfirmed = false,
            //    PhoneNumberConfirmed = false,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 0
            //};
            //userManager.Create(userToSeed);
            //context.Users.Add(userToSeed);
            //context.SaveChanges();
        }
    }
}
