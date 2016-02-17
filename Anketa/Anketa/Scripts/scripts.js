// Here Be Dragons // Like Real Much
// region - function definitions
function setDefaultStatesOfElements() {
    $(".disabled").prop("disabled", true);
}
function resetAjaxMessage() {
    $(".ajaxAlertMessageDiv").remove();
    $(".alert").remove();
};
function reinitializeQuestion() {
    $(".saveQuestion").off('click');
    $(".deleteQuestion").off('click');
    reinitializeDeleteQuestion();
    reinitializeSaveQuestion();
    reinitializeQuestionTypeChange();
    initializeTooltip();
    reinitializeChoiceItemSingle();
    setDefaultStatesOfElements();
}
function reinitializeDeleteQuestion() {
    $(".deleteQuestion").on("click", function (event) {
        //backToTop();
        var questionDiv = $(this).closest(".questionsDiv");
        deleteQuestion(questionDiv);
    });
}
function reinitializeDeleteChoiceItemSingle() {
    $(".removeChoice").on("click", function (event) {
        //backToTop();
        var choiceDiv = $(this).closest(".answersDiv");
        deleteChoiceItem(choiceDiv);
    });
}
function reinitializeSaveQuestion() {
    $(".saveQuestion").on("click", function (event) {
        var questionDiv = $(this).closest(".questionsDiv");
        saveQuestion(questionDiv);
    });
}

function reinitializeQuestionTypeChange() {
    $('.questionType').on('change', function (event) {
        var selectedOption = $(this).val();
        var questionDiv = $(this).closest('.questionsDiv');
        if (parseInt(selectedOption) == 0) {
            setQuestionTemplateDescription(questionDiv);
        } else if (parseInt(selectedOption) == 1) {
            setQuestionTemplateMultiple(questionDiv);
        } else if (parseInt(selectedOption) == 2) {
            setQuestionTemplateSingle(questionDiv);
        } else {
            setQuestionTemplateScale(questionDiv);
        }
    })
}
function reinitializeChoiceItemSingle() {
    $(".removeChoice").off('click');
    reinitializeDeleteChoiceItemSingle();
    setDefaultStatesOfElements();
}


function findOrderForQuestion(questionDiv) {
    var orderCount = 0;
    $('.questionsDiv').each(function () {
        orderCount++;
        if ($(this).find('input[name = "__RequestVerificationToken"]').val() == questionDiv.find('input[name = "__RequestVerificationToken"]').val()) {
            return false;
        }
    })
    return orderCount;
}

function initializeTooltip() {
    $('.tooltipControl').tooltip('destroy');
    $('.tooltipControl').tooltip({ title: "Deleting a question will delete all data linked to it.", trigger: 'hover', 'delay': { show: 500, hide: 300 } });
    //$('.tooltipControl').each(function () {
        //var deleteButton = $(this).find('.deleteQuestion');
        //if(deleteButton.hasClass('disabled')){
            //$(this).tooltip({ title: "Deleting a question will delete all data linked to it.", trigger: 'hover', 'delay': { show: 500, hide: 300 } });
        //}else{
        //    $(this).tooltip('destroy');
        //}
    //});
}

function backToTop() {
    $('html, body').animate({ scrollTop: '0px' }, 800).promise().then(function () { });
}

$(function () {
    $(".sortable").sortable();
    $(".sortable").disableSelection();
});

function deleteQuestion(questionDiv) {
    //var actionLink = $(this).closest("form").prop('action');
    resetAjaxMessage();
    var questionID = questionDiv.find("#questionID").val();
    if (questionID <= 0) { questionDiv.remove(); }
    else {
        var question = new Object();
        question.questionID = questionID;
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/Questions/_AjaxDeleteQuestion",
            data: JSON.stringify(question),
            dataType: "json",
            success: function (data) {
                questionDiv.remove();
                if (data.type != 1) {
                    $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
                }
                else {
                    $("#_AjaxInfoMessage").prepend('<div class ="alert alert-success ajaxAlertMessageDiv">' + data.message + "</div>")
                }
            },
            error: function (xhr, err, data) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
            }
        });
    }
};

function deleteChoiceItem(choiceDiv) {
    resetAjaxMessage();
    var choiceId = choiceDiv.find(".choiceId").val();
    if (choiceId <= 0) { choiceDiv.remove(); }
    else {
        var choiceItem = new Object();
        choiceItem.choiceId = choiceId;
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/Answers/_AjaxDeleteSingleChoice",
            data: JSON.stringify(choiceItem),
            dataType: "json",
            success: function (data) {
                choiceDiv.remove();
                if (data.type != 1) {
                    $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
                }
                else {
                    $("#_AjaxInfoMessage").prepend('<div class ="alert alert-success ajaxAlertMessageDiv">' + data.message + "</div>")
                }
            },
            error: function (xhr, err, data) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
            }
        });
    }
};

function saveQuestion(questionDiv) {
    resetAjaxMessage();
    var surveyID = $("#surveyID").val();
    var question = {};
    var answer = {};
    var answerList = new Array();
    var answerChoiceList = new Array();
    var orderCountChoice = 0;
    questionDiv.find('div.actualQuestion :input').serializeArray().map(function (x) {
        question[x.name] = x.value;
    });
    questionDiv.find('div.answersDiv :input').filter('.answerPart').serializeArray().map(function (x) {
        answer[x.name] = x.value;
    });
    answer["answerID"] = questionDiv.find('#answerID').val();
    answer["questionType"] = questionDiv.find('#questionType').val();
    answer["questionID"] = question["questionID"];

    question["surveyID"] = surveyID;
    question["aktivnoPitanje"] = questionDiv.find('#aktivnoPitanje').prop('checked');
    question["questionOrder"] = findOrderForQuestion(questionDiv);
    question["questionType"] = questionDiv.find('#questionType').val();

    var answerChoices;
    if (question["questionType"] != "0") {
        if (question["questionType"] != "3") {
            if (question["questionType"] == "1") {
                answerChoices = questionDiv.find('.multipleAnswers').find('.choiceItem');
            } else if (question["questionType"] == "2") {
                answerChoices = questionDiv.find('.singleAnswers').find('.choiceItem');
            }

            answerChoices.each(function () {
                orderCountChoice++;
                var answerChoiceItem = {};
                answerChoiceItem["choiceId"] = $(this).find('#choiceId').val();
                answerChoiceItem["choiceText"] = $(this).find('.choiceText').val();
                answerChoiceItem["orderNo"] = orderCountChoice;
                answerChoiceItem["answerID"] = answer["answerID"];
                answerChoiceList.push(answerChoiceItem);
            });

            if (question["questionType"] == "1") {
                answer["selectAnswers"] = answerChoiceList;
            } else {
                answer["radioAnswers"] = answerChoiceList;
            }
        }
        answerList.push(answer);
        question["answer"] = answerList;
    }

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Questions/_AjaxSaveQuestion",
        data: JSON.stringify(question),
        dataType: "json",
        success: function (data) {
            if (data.type != 1) {
                questionDiv.prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>");
            }
            else {
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-success ajaxAlertMessageDiv">' + data.message + "</div>")
                if (data.questionId != 0) {
                    questionDiv.find('#questionID').val(data.questionId);
                }
                if (data.answerId != 0) {
                    questionDiv.find('#answerID').val(data.answerId);
                }
                //questionDiv.find('.deleteQuestion').addClass('disabled');
                questionDiv.find('#questionType').addClass('disabled');
                $(".disabled").prop("disabled", true);
                initializeTooltip();
                backToTop();
            }
        },
        error: function (xhr, err, data) {
            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
            $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
        }
    });
};

function editSurvey(surveysDiv) {
    resetAjaxMessage();
    var survey = {};
    surveysDiv.find('form').serializeArray().map(function (x) {
            survey[x.name] = x.value;
    });
    survey['surveyActive'] = surveysDiv.find('#surveyActive').prop('checked');
    // iz nekog fing razloga je počeo stvarat input type="hidden" ispod samog checkboxa... Naravno da je konstantna vrijednost toga false...
    // ne znam samo kako/zašto je prije radio
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Surveys/_AjaxEdit",
        data: JSON.stringify(survey),
        dataType: "json",
        success: function (data) {
            if (data.type == 2) {
                surveysDiv.prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>");
            }
            if (data.type == 0) {
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>");
            }
            if (data.type == 1) {
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-success ajaxAlertMessageDiv">' + data.message + "</div>");
            }
        },
        error: function (xhr, err, data) {
            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
            $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
        }
    });
}

function setQuestionTemplateDescription(questionDiv) {
    questionDiv.find('.descriptionAnswer').prop('hidden', false);
    questionDiv.find('.scaleAnswers').prop('hidden', true);
    questionDiv.find('.singleAnswers').prop('hidden', true);
    questionDiv.find('.multipleAnswers').prop('hidden', true);
}

function setQuestionTemplateScale(questionDiv) {
    questionDiv.find('.descriptionAnswer').prop('hidden', true);
    questionDiv.find('.scaleAnswers').prop('hidden', false);
    questionDiv.find('.singleAnswers').prop('hidden', true);
    questionDiv.find('.multipleAnswers').prop('hidden', true);
}

function setQuestionTemplateSingle(questionDiv) {
    questionDiv.find('.descriptionAnswer').prop('hidden', true);
    questionDiv.find('.scaleAnswers').prop('hidden', true);
    questionDiv.find('.singleAnswers').prop('hidden', false);
    questionDiv.find('.multipleAnswers').prop('hidden', true);
}

function setQuestionTemplateMultiple(questionDiv) {
    questionDiv.find('.descriptionAnswer').prop('hidden', true);
    questionDiv.find('.scaleAnswers').prop('hidden', true);
    questionDiv.find('.singleAnswers').prop('hidden', true);
    questionDiv.find('.multipleAnswers').prop('hidden', false);
}

// endregion - function definitions


// region Document.ready
$(document).ready(function () {

    //$('form').each(function () {
    //    $(this).validate();
    //    $(this).removeAttr("novalidate");
    //});

    $('#all-surveys').DataTable();
    $('#my-surveys').DataTable();
    setDefaultStatesOfElements();
    initializeTooltip();

    $(".backToTop").click(function (e) {
        backToTop();
    });
    // #backToTop button Start
        //plugin
    jQuery.fn.topLink = function (settings) {
        settings = jQuery.extend({
            min: 1,
            fadeSpeed: 200
        }, settings);
        return this.each(function () {
            //listen for scroll
            var el = $(this);
            el.hide(); //in case the user forgot
            $(window).scroll(function () {
                if ($(window).scrollTop() >= settings.min) {
                    el.fadeIn(settings.fadeSpeed);
                }
                else {
                    el.fadeOut(settings.fadeSpeed);
                }
            });
        });
    };
        //usage w/ smoothscroll
        //set the link
        $('#backToTop').topLink({
            min: 100,
            fadeSpeed: 600
        });
        //smoothscroll
        $('#backToTop').click(function (e) {
            //    e.preventDefault(); // this part requires some scrollTo plugin
            //    $.scrollTo(0, 300);
            $('html, body').animate({ scrollTop: '0px' }, 800).promise().then(function () { });
        });
    // #backToTop button End

    // #delete question function
        $(".deleteQuestion").click(function (event) {
            var questionDiv = $(this).closest(".questionsDiv");
            deleteQuestion(questionDiv);
        });

    // #delete choiceItem function
        $(".removeChoice").click(function (event) {
            var choiceDiv = $(this).closest(".singleChoiceItem");
            deleteChoiceItem(choiceDiv);
        });
        
    // #save question function
        $(".saveQuestion").click(function (event) {
            var questionDiv = $(this).closest(".questionsDiv");
            saveQuestion(questionDiv);
        });

        $(".editSurvey").click(function (event) {
            var surveysDiv = $(this).closest(".surveysDiv");
            editSurvey(surveysDiv);
        });

        //$(".addSingle").click(function (event) {
        //    var target = $(this).val();
        //    var choiceList = $(this).closest('.answersDiv').find('ul#' + target);
        //    $.ajax({
        //        type: "POST",
        //        contentType: "application/json",
        //        url: "/Answers/_AjaxAddChoiceItemSingle",
        //        dataType: "json",
        //        success: function (data) {
        //            $.post(data.Url, function(partial) { 
        //                choiceList.append(partial);
        //            });
        //        },
        //        error: function (xhr, err, data) {
        //            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
        //            alert("responseText: " + xhr.responseText);
        //            $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
        //        }
        //    });
        //});

    $(".enter-survey-questions .dropdown-menu li a").click(function () {
        $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>');
        $(this).parents(".dropdown").find('.btn').val($(this).data('value'));
    });


    $(document).on("click", ".duplicate-input-group", function () {
        if($(this).parent(".panel-footer").prev(".panel-body").find(".answer").is(":hidden")){
            $(this).parent(".panel-footer").prev(".panel-body").find(".answer:hidden").show();
        }else{
            $(this).parent(".panel-footer").prev(".panel-body").find(".answer:first").clone().appendTo($(this).parent(".panel-footer").prev(".panel-body")).show();
        }
    });

    $(document).on("click", ".delete-answer", function () {
        console.log($(this).parents(".panel-body").find(".answer").length);
        if ($(this).parents(".panel-body").find(".answer").length == 1) {
            $(this).parents(".answer").hide();
        } else {
            $(this).parents(".answer").remove();
        }
    });
});
// endregion Document.ready