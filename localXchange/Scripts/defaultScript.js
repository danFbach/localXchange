$(document).ready(function () {
    $(".control-label").each(function () {
        if ($(this).hasClass(".R")) {
            var curText = $(this).html();
            $(this).html(curText + " *");
        }
    });
});