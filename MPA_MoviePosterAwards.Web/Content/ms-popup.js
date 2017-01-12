
(function ($) {

    var $popup,
        $cover = $('.ms-popup-cover'),
        $container = $('.ms-popup-contents');

    //弹出层居中
    $(document).ready(function () {
        $('.ms-popup-body').each(function () {
            var popupheight = $(this).height() * 10 / 9;
            var popupwidth = $(this).width() * 10 / 9;
            $(this).css('margin-top', -popupheight / 2);
            $(this).css('margin-left', -popupwidth / 2);
        });
    });

    var methods = {
        dockeyup:
            // 按 Esc 键时关闭 popup
            function onDocumentKeyUp(event) {
                if (event.keyCode === 27) {
                    methods.deactivate();
                }
            },
        docclick:
            // 鼠标单击 popup 外区域时关闭 popup
            function onDocumentClick(event) {
                if ($(event.target).hasClass('ms-popup-cover')) {
                    methods.deactivate();
                }
            },
        activate:
            function activate() {
                $(document).bind('keyup', methods.dockeyup);
                $(document).bind('click', methods.docclick);
                $container.addClass('active');
                $cover.addClass('active');
                $popup.addClass('active');
            },
        deactivate:
            function deactivate() {
                $(document).unbind('keyup', methods.dockeyup);
                $(document).unbind('click', methods.docclick);

                $container.removeClass('active');
                $cover.removeClass('active');
                $popup.removeClass('active');
            },
        show: function () {
            $popup = this;
            methods.activate($(this));
        },
        hide: function () {
            $popup = this;
            methods.deactivate();
        }
    };

    $.fn.msPopup = function (method) {

        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        }
        else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        }
        else {
            $.error('Method ' + method + ' does not exist on msPopup');
        }
    };

})(jQuery);