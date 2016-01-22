using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

//using Survey = Anketa.Models.Survey;
//using Question = Anketa.Models.Question;
//using Answer = Anketa.Models.Answer;

using Anketa.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity; // Ako se ne koristi ovo enumeracija zahtijeva dodavanje Anketa.Models. ispred naziva, praktičnost.

namespace Anketa.DAL
{
    public class SurveyInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SurveyContext>
    //DropCreateDatabaseAlways
    //CreateDatabaseIfNotExists
    //DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(SurveyContext context)
        {
            var surveys = new List<Survey>
            {
                //creationDate=DateTime.Parse("2015-09-01")
                new Survey{surveyID=1,ownerID=1,surveyName="Prva Anketa",creationDate=DateTime.Now, editDate = DateTime.Now},
                new Survey{surveyID=2,ownerID=1,surveyName="Druga Anketa",creationDate=DateTime.Now, editDate = DateTime.Now},
                new Survey{surveyID=3,ownerID=2,surveyName="Mijenjana Anketa",creationDate=DateTime.Now, editDate = DateTime.Now}
            };

            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();

            var questions = new List<Question>
            {
                new Question{questionID=1,SurveyID=1,questionText="Prvo Pitanje",TipPitanja=TipPitanja.Single},
                new Question{questionID=2,SurveyID=2,questionText="Drugo Pitanje",TipPitanja=TipPitanja.Single},
                new Question{questionID=3,SurveyID=3,questionText="Treće Pitanje",TipPitanja=TipPitanja.Multiple}
            };

            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var answers = new List<Answer>
            {
                new Answer{answerID=1,questionID=1,answerText="Prvi Odgovor",correct=true},
                new Answer{answerID=2,questionID=1,answerText="Drugi Odgovor",correct=false},
                new Answer{answerID=3,questionID=2,answerText="Treći Odgovor",correct=true},
                new Answer{answerID=4,questionID=2,answerText="Četvrti Odgovor",correct=false},
                new Answer{answerID=5,questionID=3,answerText="Peti Odgovor",correct=true},
                new Answer{answerID=6,questionID=3,answerText="Šesti Odgovor",correct=false},
            };

            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var usersToSeed = new List<User>
            {
                new User{UserName = "Unidentified User", PasswordHash = new PasswordHasher().HashPassword("Password123!"), UserProfileInfo = new UserProfileInfo { Id = 1, userName = "Unidentified User" }},
                new User{UserName = "Dummy", PasswordHash = new PasswordHasher().HashPassword("Password1234!"), Email = "dummy@gmail.com", UserProfileInfo = new UserProfileInfo { Id = 2, userName = "Dummy" }},
            };

            usersToSeed.ForEach(s => context.Users.Add(s));
            // da bi mogli insertati usera s kojim se moguće ulogirati, treba nam async Task, kao u account controlleru u register
        }

    }
}