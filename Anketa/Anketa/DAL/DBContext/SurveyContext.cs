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
using Anketa.Models.AnswerModels;
using Anketa.Models.SurveySolvedModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anketa.DAL
{

    //http://www.entityframeworktutorial.net/code-first/inverseproperty-dataannotations-attribute-in-code-first.aspx
    public class SurveyContext : IdentityDbContext<User>
    {
        public SurveyContext() : base("SurveyContext") 
        {
            //Database.SetInitializer<SurveyContext>(null);
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        public static SurveyContext Create()
        {
            return new SurveyContext();
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerChoiceSingle> AnswerChoiceSingle { get; set; }
        public DbSet<AnswerChoiceMultiple> AnswerChoiceMultiple { get; set; }
        //public DbSet<UserProfileInfo> UserProfileInfo { get; set; }

        public DbSet<SurveySolved> SurveySolved { get; set; }
        //public DbSet<AnswerChosen> AnswerChosen { get; set; }
        //public DbSet<ChosenMultipleChoice> ChosenMultipleChoice { get; set; }
        //public DbSet<ChosenSingleChoice> ChosenSingleChoice { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<AnswerChoice>()
            .HasKey(p => p.choiceId)
            .Property(p => p.choiceId)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<ChosenSingleChoice>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("ChosenSingleChoice");
            });

            modelBuilder.Entity<ChosenMultipleChoice>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("ChosenMultipleChoice");
            });

        }
    }
}