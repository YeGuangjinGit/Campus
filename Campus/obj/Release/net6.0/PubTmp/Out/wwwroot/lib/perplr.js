$(function () {
    //鼠标移出隐藏
    $('.div_tx').mouseout(function () {
        $('#img_tx').show();
    })
    //鼠标移入显示
    $('.div_tx').mouseover(function () {
        $('#img_tx').hide();
    })

    $('#qm_txt').click(function () {
        $(this).css("color", "black");
    })
})
