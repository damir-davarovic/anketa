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

namespace Anketa.DAL
{
    public class SurveyContext : DbContext
    {
        public SurveyContext()
            : base("SurveyContext") //DefaultConnection ime konekcijskog stringa unutar web.config filea
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}