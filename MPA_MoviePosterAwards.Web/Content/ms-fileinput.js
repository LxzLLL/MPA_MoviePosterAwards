

$(document).ready(function () {
    $('input[type="file"]').each(function () {
        if ($(this).attr('accept') != null && $(this).attr('accept').indexOf('image') != -1)
            $(this).after('<label for="' + $(this).prop('id') + '">选择图片<span>...</span></label>');
        else
            $(this).after('<label for="' + $(this).prop('id') + '">选择文件<span>...</span></label>');
    });
});

$('input[type="file"]').change(function () {
    //console.log('select file change');
    console.log($(this).val());
    if ($(this).val().length > 0)
        $(this).siblings('label').children('span').html($(this).val().substring($(this).val().lastIndexOf('\\') + 1, $(this).val().length));
    else
        $(this).siblings('label').children('span').html('...');
});