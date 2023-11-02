$(function () {
    $("#changes_type span").slide(slide);
    $("#buttonn li").hide();
    $("#li_border").hide();
    $("#buttonn").click(function () {
        $("#buttonn li").show();
        $("#li_border").show();
        $("#buttonn li").css("height", "20px");
        $("#buttonn li").css("opacity", "1");
        $("#buttonn li").css("font-size", "13px");
        $("#buttonn li").css("margin-right", "20px");
        $("#buttonn li").addClass("test");
        $("#li_border").addClass("testt");
    })
    $("#buttonn").mouseleave(function () {
        $("#buttonn li").hide();
        $("#li_border").hide();
    })
})