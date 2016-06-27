(function ($) {

    $('mark').hover(function () {
        $(this).attr('title', function () {
            return $(this).attr('elem-name');
        })
    }, function () {
        $(this).removeAttr('title');
    });

}(jQuery))