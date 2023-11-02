$(function () {
    //图片轮播
    var item = new Array();
    var num = 0;
    item[0] = "img/logo1.jpg"
    item[1] = "img/logo2.jpg"
    item[2] = "img/logo3.jpg"
    item[3] = "img/logo4.jpg"
    item[4] = "img/News_logo.jpg"
    setInterval(function () {
        num++;
        if (num == 5) {
            num = 0;
        }
        $("#School_DaoHang").css("background-image", "url(" + item[num] + ")")
    }, 3000)//换图时间

    $(document).scrollTop(100);
    // 被卷去的头部 scrollTop()  / 被卷去的左侧 scrollLeft()
    // 页面滚动事件
    var boxTop = $("#School_News").offset().top;
    $(window).scroll(function () {
        // console.log(11);
        console.log($(document).scrollTop());
        if ($(document).scrollTop() >= boxTop) {
            $("#back").fadeIn();
        } else {
            $("#back").fadeOut();
        }
    });
    // 返回顶部
    $("#back").click(function () {
        // $(document).scrollTop(0);
        $("body, html").stop().animate({
            scrollTop: 0
        });
        // $(document).stop().animate({
        //     scrollTop: 0
        // }); 不能是文档而是 html和body元素做动画
    })

})