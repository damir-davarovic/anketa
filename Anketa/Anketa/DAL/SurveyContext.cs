using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Survey = Anketa.Models.Survey;
using Question = Anketa.Models.Question;
using Answer = Anketa.Models.Answer;
using User = Anketa.Models.User;
using Anketa.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anketa.DAL
{
    public class SurveyContext : IdentityDbContext<User>
    {
        public SurveyContext() : base("SurveyContext") 
        {
            Database.SetInitializer<SurveyContext>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static SurveyContext Create()
        {
            return new SurveyContext();
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserProfileInfo> UserProfileInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}