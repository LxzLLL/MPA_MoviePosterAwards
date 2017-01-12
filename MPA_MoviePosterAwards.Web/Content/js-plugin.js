

(function ($) {

    $(document).ready += initBoxs();

    function initBoxs() {
        var count = $('.ms-squeezebox>ul li').length;
        var perwidth = 100 / count;
        $('.ms-squeezebox>ul li').css('width', perwidth + '%');
        $('.ms-squeezebox>ul li').removeClass('squeeze');
        $('.ms-squeezebox>ul li').removeClass('spread');
        $('.ms-squeezebox>ul li').each(
            function (i, item) {
                $(this).css('left', perwidth * i + '%');
                $(this).css('z-index', count - i);
            }
        );
    }

    $.fn.msSqueezeBox = function () {

        var switchfinish = true;
        var listitem = $(this).children
        $('.ms-squeezebox>ul li').click(function () {
            if (!switchfinish)
                return;
            switchfinish = false;

            if ($(this).hasClass('spread')) {
                initBoxs();
                //$('.ms-squeezebox>ul li').addClass('squeeze');
                //$(this).removeClass('squeeze');
                //$(this).removeClass('spread');
            }
            else {
                var count = $('.ms-squeezebox>ul li').length;
                $('.ms-squeezebox>ul li').addClass('squeeze');
                $('.ms-squeezebox>ul li').removeClass('spread');
                $(this).removeClass('squeeze');
                $(this).addClass('spread');
                var perwidth = 5;
                var left = 0;
                var zindex = count;
                var reachedthis = false;
                $('.ms-squeezebox>ul li').each(
                    function (i, item) {
                        $(this).css('left', left + '%');
                        if (!$(this).hasClass('spread')) {
                            $(this).css('width', perwidth + '%');
                            left += perwidth;
                        }
                        else {
                            $(this).css('width', (100 - perwidth * (count - 1)) + '%');
                            left += 100 - perwidth * (count - 1);
                            reachedthis = true;
                        }
                        $(this).css('z-index', zindex);
                        if (reachedthis)
                            zindex--;
                        else
                            zindex++;
                    }
                );
            }
            setTimeout(function () {
                switchfinish = true;
            }, 500);
            return false;
        });
    };
})(jQuery);