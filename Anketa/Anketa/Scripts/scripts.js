// region - function definitions
function resetAjaxMessage() {
    $(".ajaxAlertMessageDiv").remove();
};
function reinitializeQuestion() {
    reinitializeDeleteQuestion();
    reinitializeSaveQuestion();
}
function reinitializeDeleteQuestion() {
    $(".deleteQuestion").on("click", function (event) {
        backToTop();
        var questionDiv = $(this).closest(".questionsDiv");
        deleteQuestion(questionDiv);
    });
}
function reinitializeSaveQuestion() {
    $(".saveQuestion").on("click", function (event) {
        backToTop();
        var questionDiv = $(this).closest(".questionsDiv");
        saveQuestion(questionDiv);
    });
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
function saveQuestion(questionDiv) {
    resetAjaxMessage();
    var surveyID = $("#surveyID").val();
    var question = {};
    questionDiv.find('form').serializeArray().map(function (x) { question[x.name] = x.value; });
    question["surveyID"] = surveyID;
    question["aktivnoPitanje"] = questionDiv.find('#aktivnoPitanje').prop('checked');
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Questions/_AjaxSaveQuestion",
        data: JSON.stringify(question),
        dataType: "json",
        success: function (data) {
            if (data.type != 1) {
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
            }
            else {
                $("#_AjaxInfoMessage").prepend('<div class ="alert alert-success ajaxAlertMessageDiv">' + data.message + "</div>")
                if (data.questionId != 0) {
                    questionDiv.find('#questionID').val(data.questionId);
                }
                
            }
        },
        error: function (xhr, err, data) {
            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
            $("#_AjaxInfoMessage").prepend('<div class ="alert alert-danger ajaxAlertMessageDiv">' + data.message + "</div>")
        }
    });
};

// endregion - function definitions


// region Document.ready
$(document).ready(function () {
    $('#all-surveys').DataTable();
    $('#my-surveys').DataTable();
    $(".disabled").prop("disabled", true);
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
        
    // #save question function
        $(".saveQuestion").click(function (event) {
            var questionDiv = $(this).closest(".questionsDiv");
            saveQuestion(questionDiv);
        });

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