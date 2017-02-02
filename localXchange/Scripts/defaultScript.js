$(document).ready(function () {
    $("label.control-label").each(function () {
        if ($(this).hasClass("required")) {
            var txt = $(this).text();
            $(this).html(txt + " *");
        }
    });
});