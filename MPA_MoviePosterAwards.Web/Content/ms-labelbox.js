
$(document).ready(function () {
    $('.ms-labelbox>input').each(function () {
        if ($(this).attr('type') == 'checkbox') {
            $(this).after('<span class="checkbox"></span>');
        } else {
            $(this).after('<span class="radiobutton"></span>');
        }
    });

    $('.ms-labelbox>input').change(function () {
        $('.ms-labelbox').removeClass('checked');
        $('.ms-labelbox>input:checked').each(function () {
            $(this).parent().addClass('checked');
        });
    });
});