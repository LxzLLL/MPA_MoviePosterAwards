
//为每个 .ms-squeezebox 注册 msSqueezeBox
$(document).ready(function () {
    $('.ms-squeezebox').each(function () {
        var options = { 'minwidth': 6 }
        $(this).msSqueezeBox(options);
    });
});

(function ($) {

    //初始化每个 .ms-squeezebox 的width、zindex
    $(document).ready(function () {
        $('.ms-squeezebox').each(function () {
            var count = $(this).children('ul').children('li').length;
            var perwidth = 100 / count;
            $(this).children('ul').children('li').css('width', perwidth + '%');
            $(this).children('ul').children('li').removeClass('squeeze');
            $(this).children('ul').children('li').removeClass('spread');
            $(this).children('ul').children('li').each(function (i, item) {
                $(this).css('left', perwidth * i + '%');
                $(this).css('z-index', count - i);
            });
        });
    });

    //li 内的文本选中时不响应 li 的mouseup时间，不关闭 li
    //$('.ms-squeezebox>ul>li>').on('mouseup', 'span', function () {
    //    return false;
    //});

    $.fn.msSqueezeBox = function (options) {

        var switchfinish = true;//防止同时单击多个item
        var offsetx = -1, offsety = -1;//用来判断 mousedown 和 mouseup 是否在同一点，用 mousedown+mouseup 代替 li 的 click
        var $squeezebox = this;
        var count = $(this).children('ul').children('li').length;
        var perwidth = 100 / count;
        var $settings = {
            'minwidth': 5
        };

        $(this).children('ul').on('mousedown', 'li', function (event) {
            offsetx = event.offsetX;
            offsety = event.offsetY;
        });

        $(this).children('ul').on('mouseup', 'li', function (event) {
            if (!switchfinish || !(offsetx == event.offsetX && offsety == event.offsetY))
                return;
            offsetx = -1;
            offsety = -1;
            switchfinish = false;

            //单击展开了的item，回到初始状态
            if ($(this).hasClass('spread')) {
                $squeezebox.children('ul').children('li').each(function (i, item) {
                    //console.log(count - i);
                    $(this).css('width', perwidth + '%');
                    $(this).removeClass('squeeze');
                    $(this).removeClass('spread');
                    $(this).css('left', perwidth * i + '%');
                    $(this).css('z-index', count - i);
                });
            }
            else {
                $squeezebox.children('ul').children('li').addClass('squeeze');
                $squeezebox.children('ul').children('li').removeClass('spread');
                $(this).removeClass('squeeze');
                $(this).addClass('spread');
                var left = 0;
                var zindex = count;
                var reachedthis = false;
                $squeezebox.children('ul').children('li').each(function (i, item) {
                    $(this).css('left', left + '%');
                    if (!$(this).hasClass('spread')) {
                        $(this).css('width', $settings.minwidth + '%');
                        left += $settings.minwidth;
                    }
                    else {
                        $(this).css('width', (100 - $settings.minwidth * (count - 1)) + '%');
                        left += 100 - $settings.minwidth * (count - 1);
                        reachedthis = true;
                    }
                    $(this).css('z-index', zindex);
                    if (reachedthis)
                        zindex--;
                    else
                        zindex++;
                });
            }
            setTimeout(function () {
                switchfinish = true;
            }, 500);
            return false;
        });

        return this.each(function () {
            if (options) {
                $.extend($settings, options);
            }
        });

    };
})(jQuery);