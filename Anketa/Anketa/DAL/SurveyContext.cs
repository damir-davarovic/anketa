using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Survey = Anketa.Models.Survey;
using Question = Anketa.Models.Question;
using Answer = Anketa.Models.Answer;

namespace Anketa.DAL
{
    public class SurveyContext : DbContext
    {
        public SurveyContext() : base("SurveyContext")
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}