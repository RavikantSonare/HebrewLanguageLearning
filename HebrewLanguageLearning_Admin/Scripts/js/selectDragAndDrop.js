
$(function () {
    function moveItems(origin, dest) {
        $(origin).find(':selected').appendTo(dest);
    }

    $('#leftbtn').click(function () {
        if (isValid()) {
            moveItems('#rightSelector', '#leftSelector');
        }
    });

    $('#rightbtn').on('click', function () {
        if (isValid()) {
            moveItems('#leftSelector', '#rightSelector');
        }
    });
});

var isValid = function () {
    var ddlLessonId = $('#LessonId').val();
    $('#LessonIdErrorMessage').hide();
    if (typeof ddlLessonId == 'undefined' || ddlLessonId == '0') {
        $('#LessonIdErrorMessage').show();
        return false;
    }
    return true;

}

var getDetail = function (x, baseUrl) {
        $.ajax({ url: baseUrl, method: 'post', data: { LessonId: x, __RequestVerificationToken: document.getElementsByName('__RequestVerificationToken')[0].value } }).done(function (res) {
            $('#rightSelector').html('');
            $.each(res, function (i, v) {
                $('#rightSelector').append('<option value="' + v["HebrewApplicationData"] + '">' + v["HebrewApplicationData"] + '</option>')
            });
        });
    };

