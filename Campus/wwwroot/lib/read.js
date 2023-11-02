$('.speak_back').click(function () {
	$(this).parent().siblings('div:last-child').toggle()
})
$('#good3').hover(function () {
	$(this).children('img').attr('src', '/img/gouse_1.png')
}, function () {
	$(this).children('img').attr('src', '/img/gouse_2.png')
})
$('#good4').hover(function () {
	$(this).children('img').attr('src', '/img/index_1.png')
}, function () {
	$(this).children('img').attr('src', '/img/index_2.png')
})
let iRet = true;
let iRet2 = true;
let iRet3 = true;
$('.good1').click(function () {

	if (iRet == true) {
		$(this).attr('src', '/img/good_1.png')
		iRet = false
	}
	else {
		$(this).attr('src', '/img/good_2.png')
		iRet = true
	}
})
$('.good2').click(function () {
	if (iRet2 == true) {
		$(this).attr('src', '/img/mine_1.png')
		iRet2 = false
	} else {
		$(this).attr('src', '/img/mine_2.png')
		iRet2 = true
	}
})