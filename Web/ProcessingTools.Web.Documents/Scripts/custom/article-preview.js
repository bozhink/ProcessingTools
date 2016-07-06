(function ($) {

    $('mark').hover(function () {
        $(this).attr('title', function () {
            return $(this).attr('elem-name');
        })
    }, function () {
        $(this).removeAttr('title');
    });

    $('.named-content.geo-json').each(function () {
        var coordinates = JSON.parse($(this).attr('specific-use'))['coordinates'];
        console.log(coordinates)
    })

}(jQuery))