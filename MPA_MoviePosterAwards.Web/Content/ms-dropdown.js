

//为每个 .ms-dropdown 注册 msDropdown
$(document).ready(function () {
    $('.ms-dropdown').each(function () {
        $(this).msDropdown();
    });
});

(function ($) {

    $.fn.msDropdown = function () {
        var $ul = this.children('ul');
        var $dropdown = this;
        var $span = this.children('span');

        $span.on('click', function () {
            //先把其他的 dropdown 关闭
            $('.ms-dropdown').each(function () {
                if ($(this).prop('id') != $dropdown.prop('id')) {
                    $(this).removeClass('open');
                    $(this).children('ul').removeClass('slideopen');
                    $(this).children('ul').addClass('slideclose');
                }
            });
            //console.log('ms-dropdown click');
            switchlist();
            switcharrow();
            return false;//阻止事件冒泡 到 body click
        });

        //单击 dropdown 的 item 后关闭 dropdown
        $ul.on('click', 'li', function () {
            //console.log('li click');
            switchlist();
            switcharrow();

        });

        //单击 dropdown 外的区域后关闭 dropdown
        $('body').on('click', function () {
            //console.log('body click');
            $ul.addClass('slideclose');
            $ul.removeClass('slideopen');
            $dropdown.removeClass('open');
        });

        function switchlist() {
            if ($ul.hasClass('slideopen')) {
                $ul.addClass('slideclose');
                $ul.removeClass('slideopen');
            }
            else {
                $ul.removeClass('slideclose');
                $ul.addClass('slideopen');
            }
        }

        function switcharrow() {
            //console.log('switch arrow');
            if ($dropdown.hasClass('open'))
                $dropdown.removeClass('open');
            else
                $dropdown.addClass('open');
        }

    };
})(jQuery);


(function ($) {

    //为每个 .ms-hoverdown 注册 msHoverdown
    $(document).ready(function () {
        //console.log('注册 mousedown');
        $('.ms-hoverdown').each(function () {
            var options = { 'select': true };
            $(this).msHoverdown(options);
        });
    });

    $.fn.msHoverdown = function (options) {

        //console.log(options);
        var $ul = this.children('ul');
        var $hoverdown = this;
        var $span = this.children('span');
        var $settings = {
            'select': false
        };

        $span.on('mouseover', function () {
            $('.ms-hoverdown').each(function () {
                $(this).removeClass('open');
                $(this).children('ul').removeClass('slideopen');
                $(this).children('ul').addClass('slideclose');
            });
            listopen();
            arrowopen();
            return false;//阻止事件冒泡 到 body mouseover
        });

        //当鼠标在下拉ul中时，阻止事件冒泡 到 body mouseover
        $ul.on('mouseover', function () {
            return false;
        });

        //单击 dropdown 的 item 后关闭 dropdown
        $ul.on('click', 'li', function () {
            if ($settings.select)
                $span.html($(this).html());
            listclose();
            arrowclose();
        });

        //鼠标在 dropdown 外的区域后关闭 dropdown
        $('body').on('mouseover', function () {
            listclose();
            arrowclose();
        });

        function listopen() {
            $ul.removeClass('slideclose');
            $ul.addClass('slideopen');
        }
        function listclose() {
            $ul.removeClass('slideopen');
            $ul.addClass('slideclose');
        }

        function arrowopen() {
            $hoverdown.addClass('open');
        }

        function arrowclose() {
            $hoverdown.removeClass('open');
        }


        return this.each(function () {
            if (options) {
                $.extend($settings, options);
            }
        });
    };
})(jQuery);