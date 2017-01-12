
$('.ms-navbar-nav > li > a').click(function () {
    $('.ms-navbar-nav').find('li').each(function () {
        if ($(this).hasClass('active'))
            $(this).removeClass('active');
    });
    $(this).parent().addClass('active');
});


(function ($) {

    //为每个 .ms-navbar-dropdown 注册 msNavDropdown
    $(document).ready(function () {
        //console.log('注册 mousedown');
        $('.ms-navbar-dropdown').each(function () {
            $(this).msNavDropdown();
        });
    });

    $.fn.msNavDropdown = function () {

        //console.log(options);
        var $ul = this.children('ul');
        var $hoverdown = this;
        var $span = this.children('span');

        $span.on('mouseover', function () {
            $('.ms-navbar-dropdown').each(function () {
                $(this).removeClass('slideopen');
                $(this).addClass('slideclose');
            });
            listopen();
            return false;//阻止事件冒泡 到 body mouseover
        });

        //当鼠标在下拉ul中时，阻止事件冒泡 到 body mouseover
        $ul.on('mouseover', function () {
            return false;
        });

        //单击 dropdown 的 item 后关闭 dropdown
        $ul.on('click', 'li', function () {
            listclose();
        });

        //鼠标在 dropdown 外的区域后关闭 dropdown
        $('body').on('mouseover', function () {
            listclose();
        });

        function listopen() {
            $hoverdown.removeClass('slideclose');
            $hoverdown.addClass('slideopen');
        }
        function listclose() {
            $hoverdown.removeClass('slideopen');
            $hoverdown.addClass('slideclose');
        }
    };
})(jQuery);