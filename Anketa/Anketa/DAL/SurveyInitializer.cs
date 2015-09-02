using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

//using Survey = Anketa.Models.Survey;
//using Question = Anketa.Models.Question;
//using Answer = Anketa.Models.Answer;

using Anketa.Models; // Ako se ne koristi ovo enumeracija zahtijeva dodavanje Anketa.Models. ispred naziva, praktičnost.

namespace Anketa.DAL
{
    public class SurveyInitializer : System.Data.Entity.DropCreateDatabaseAlways<SurveyContext>
        // clean - build - refresh connection on database - close connection - repeat
        // start in debug 
        // logoff - login, start tackling tables
    {
        protected override void Seed(SurveyContext context)
        {
            var surveys = new List<Survey>
            {
                new Survey{surveyID=1,ownerID="aaa",surveyName="Prva Anketa",creationDate=DateTime.Parse("2015-09-01")},
                new Survey{surveyID=2,ownerID="bbb",surveyName="Druga Anketa",creationDate=DateTime.Parse("2015-09-01")},
                new Survey{surveyID=3,ownerID="cce",surveyName="Mijenjana Anketa",creationDate=DateTime.Parse("2015-09-01")}
            };

            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();

            var questions = new List<Question>
            {
                new Question{ID=1,surveyID=1,questionText="Prvo Pitanje",TipPitanja=TipPitanja.single},
                new Question{ID=2,surveyID=2,questionText="Drugo Pitanje",TipPitanja=TipPitanja.single},
                new Question{ID=3,surveyID=3,questionText="Treće Pitanje",TipPitanja=TipPitanja.multiple}
            };

            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var answers = new List<Answer>
            {
                new Answer{ID=1,questionID=1,answerText="Prvi Odgovor",correct=true},
                new Answer{ID=2,questionID=1,answerText="Drugi Odgovor",correct=false},
                new Answer{ID=3,questionID=2,answerText="Treći Odgovor",correct=true},
                new Answer{ID=4,questionID=2,answerText="Četvrti Odgovor",correct=false},
                new Answer{ID=5,questionID=3,answerText="Peti Odgovor",correct=true},
                new Answer{ID=6,questionID=3,answerText="Šesti Odgovor",correct=false},
            };


            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();

        }

    }
}