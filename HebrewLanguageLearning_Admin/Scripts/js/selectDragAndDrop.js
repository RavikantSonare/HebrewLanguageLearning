
$(function () {
    function moveItems(origin, dest) {
        $(origin).find(':selected').appendTo(dest);
    }
    $('#leftbtn').click(function () {
        if (isValid()) {
            var _that = $('#rightSelector')["0"].value;
            var _childId = $('#rightSelector').find(':selected')["0"].value;
            moveItems('#rightSelector', '#leftSelector');
            if (ViewName = 'GrammarPageData') { setVocabularyData(_childId, 0); }
            if (ViewName = 'VocabularyPageData') { setLessonsGrammarData(_childId, 0); }
        }
    });

    $('#rightbtn').on('click', function () {
        if (isValid()) {
            var _childId = $('#leftSelector').find(':selected')["0"].value;
            moveItems('#leftSelector', '#rightSelector');
            if (ViewName = 'GrammarPageData') { setVocabularyData(_childId, 1); }
            if (ViewName = 'VocabularyPageData') { setLessonsGrammarData(_childId, 1); }
           
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


var setVocabularyData = function (_DictionaryEntriesId, _isAdd) {

    var jsonData = { __RequestVerificationToken: document.getElementsByName('__RequestVerificationToken')[0].value, isAdd: _isAdd, "VocabularyId": "12", "LessonId": ddlLessonId = $('#LessonId').val(), "DictionaryEntriesId": _DictionaryEntriesId };

    $.ajax({ url: 'AddRemoveDataInLesson', method: 'post', data: (jsonData) }).done(function (res) {
        console.log(res);
    });
};
var setLessonsGrammarData = function (_fkGrammarId, _isAdd) {

    var jsonData = {
        __RequestVerificationToken: document.getElementsByName('__RequestVerificationToken')[0].value, isAdd: _isAdd, "LessonsGrammarId": "0",
        "fkLessonId": ddlLessonId = $('#LessonId').val(), "fkGrammarId": _fkGrammarId,
        "GrammarPoint1": $('#GrammarPoint1').val(), "GrammarPoint2": $('#GrammarPoint2').val(), "GrammarPoint3": $('#GrammarPoint3').val(), "GrammarPoint4": $('#GrammarPoint4').val(),
    };

    $.ajax({ url: 'AddRemoveDataInLesson', method: 'post', data: (jsonData) }).done(function (res) {
        console.log(res);
    });
};
var ViewName = '';
$(document).ready(function () {
    ViewName = '';
    var url = "";
    if (!url) url = window.location.href;
    var pathname = new URL(url).pathname;
    var SplitData = pathname.split('/');
    ViewName = SplitData[SplitData.length - 2] + SplitData[SplitData.length - 1];
    if (ViewName == 'GrammarGrammarSelector') { ViewName = 'GrammarPageData'; }
    if (ViewName == 'VocabularyCreate') { ViewName = 'VocabularyPageData'; }
   

});
