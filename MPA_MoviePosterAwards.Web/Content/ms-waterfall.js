
(function ($) {
    var index = 0;
    var allpics = new Array();
    var methods = {
        appendlist: function (waterfallpics) {
            //将新增的图片加入到所有图片列表中去
            for (var i = 0; i < waterfallpics.length; i++) {
                allpics.push(waterfallpics[i]);
            }
            //console.log(allpics.length);
            methods.appenditem(waterfallpics);

        },
        appenditem: function (waterfallpics) {//从新增图片列表中取index添加到瀑布流中
            var columncount = $('.ms-waterfall').children('.ms-waterfall-column').length;

            var minheight = Number.MAX_VALUE, mincolumn = 0;
            for (var j = 1; j <= columncount; j++) {
                var columnheight = 0;
                $('.ms-waterfall-column.column-' + j + ' img').each(function () {
                    columnheight += $(this).height() + 15;
                });
                if (columnheight - minheight < 0) {
                    minheight = columnheight;
                    mincolumn = j;
                }
            }

            var elewaterfall = '<div><a href="/Poster/Index?id=' + waterfallpics[index].id + '"><img src="' + waterfallpics[index].url + '" title="' + waterfallpics[index].title + '" /><div>' + waterfallpics[index].title + '</div></a></div>';

            // 先缓存 图片， 保证不会因为网速跟不上，图片没有加载出来而导致 column 的高度计算错误
            // 同时保证图片按顺序一张张加载
            var imgtemp = new Image();//创建一个image对象
            imgtemp.src = waterfallpics[index].url;
            
            imgtemp.onload = function () {
                $('.ms-waterfall-column.column-' + mincolumn).append(elewaterfall);

                // 手动设置 ms-waterfall 的高度
                var columnheights = new Array();
                $('.ms-waterfall-column').each(function () {
                    columnheights.push($(this).css('height').replace('px', ''));
                });
                var maxheight = Math.max.apply(null, columnheights);
                $('.ms-waterfall').css('height', maxheight + 'px');
                index++;
                //继续加载下一张
                if (index < waterfallpics.length)
                    methods.appenditem(waterfallpics);
                else
                    index = 0;
            }
        },
        init: function () {
            //根据列数设置列宽
            var canvarwidth = $('.ms-container').css('width').replace('px', '');
            var columncount = 0;
            if (canvarwidth == 1280) {
                columncount = 6;
            }
            else if (canvarwidth == 1120) {
                columncount = 5;
            }
            else if (canvarwidth == 970) {
                columncount = 4;
            }
            else {
                columncount = 3;
            }
            $('.ms-waterfall').html('');
            for (var i = 1; i <= columncount; i++) {
                $('.ms-waterfall').append('<div class="ms-waterfall-column column-' + i + '"></div>')
            }
            var columncount = $('.ms-waterfall').children('.ms-waterfall-column').length;
            $('.ms-waterfall-column').css('width', (100 / columncount) + '%');
        }
    };

    $.fn.msWaterfall = function (method) {
        index = 0;
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