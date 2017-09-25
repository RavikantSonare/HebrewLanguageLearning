
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

