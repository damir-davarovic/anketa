$(document).ready(function () {
    $('#all-surveys').DataTable();
    $('#my-surveys').DataTable();
    $(".disabled").prop("disabled", true);
    $(".backToTop").click(function (e) {
        $('html, body').animate({ scrollTop: '0px' }, 800).promise().then(function () { });
    });
    $(".resetAjaxMessage").click(function () {
        $(".ajaxAlertMessageDiv").remove();
    });

    $(".deleteQuestion").click(function (event) {
        event.preventDefault();
        var actionLink = $(this).closest("form").prop('action');
        var questionDiv = $(this).closest(".questionsDiv");
        var questionID = questionDiv.find("#questionID").val();
        $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: actionLink,
                    data: questionID,
                    dataType: "json",
                    success: function (data) {
                        questionDiv.remove();
                    },
                    error: alert("error")
                });
    });
    //$(".deleteQuestion").on('click', function () {
    //    $(this).parent(".questionsDiv").remove();
    //});

    //$('.deleteQuestion').click(function () {
    //    var url = "Questions/_AjaxDeleteQuestion";
    //    var questionDiv = $(this).closest(".questionsDiv");
    //    var questionID = questionDiv.find("#questionID").val();
    //    $.ajax({
    //        type: "POST",
    //        contentType: "application/json; charset=utf-8",
    //        url: '<%= Url.Action("_AjaxDeleteQuestion", "Questions", ) %> ',
    //        data: questionID,
    //        dataType: "json",
    //        success: function (data) {
    //            alert("success");
    //            questionDiv.remove();
    //        },
    //        error: alert("error")
    //    });
    //});
 
    // backToTop button Start

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
    $(document).ready(function () {
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
    });

    // backToTop button End

    $(".deleteQuestion").click(function () {
        $(this).parent(".questionsDiv").remove();
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