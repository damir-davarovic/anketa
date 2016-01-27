using Anketa.DAL;
using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public enum TipPitanja
    {
        Description, Multiple, Single, Scale
    }

    public class Question : IValidatableObject
    {
        [Index(IsUnique = true)]
        public int questionID { get; set; }
        public int SurveyID { get; set; }
        [DisplayName("Question text"), Required(ErrorMessage = "Question text is required!")]
        public string questionText { get; set; }
        [DisplayName("Question type"), Required ]
        public TipPitanja questionType { get; set; }
        [DisplayName("Question active")]
        public bool aktivnoPitanje { get; set; }
        public bool hasAnswer { get; set; }
        public int questionOrder { get; set; }

       public virtual ICollection<Answer> answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            SurveyContext surveyContext = new SurveyContext();
            if (questionText == null || questionText.Trim() == "")
            {
                yield return new ValidationResult("Question text is required!");
            }
            if(answer  != null)
            {
                if (questionType.Equals(TipPitanja.Single) || questionType.Equals(TipPitanja.Multiple))
                {
                    Answer qAnswer = answer.First();
                    bool answerChoicesInValid = false;
                    if (qAnswer.selectAnswers != null)
                    {
                        foreach (AnswerChoiceMultiple answerChoice in qAnswer.selectAnswers)
                        {
                            if (answerChoice.choiceText.Trim().Length < 1)
                            {
                                answerChoicesInValid = true;
                            }
                        }
                    }
                    if (qAnswer.radioAnswers != null)
                    {
                        foreach (AnswerChoiceSingle answerChoice in qAnswer.radioAnswers)
                        {
                            if (answerChoice.choiceText.Trim().Length < 1)
                            {
                                answerChoicesInValid = true;
                            }
                        }
                    }
                    if (answerChoicesInValid)
                    {
                        yield return new ValidationResult("Choice text is required");
                    }
                }
                if (questionType.Equals(TipPitanja.Scale))
                {
                    Answer qAnswer = answer.First();
                    if (qAnswer.minAnswerValue >= qAnswer.maxAnswerValue)
                    {
                        yield return new ValidationResult("Maximum scale value should be bigger than minimum value!");
                    }
                }
            }
            
        }
    }
}