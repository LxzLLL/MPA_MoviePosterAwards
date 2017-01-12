
$(window).scroll(function () {
    console.log('scroolllll');
    $('.ms-banner-background').css('top', $(window).scrollTop() / 2);
});