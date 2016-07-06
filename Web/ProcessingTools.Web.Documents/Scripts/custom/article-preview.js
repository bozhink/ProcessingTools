(function ($) {

    $('mark').hover(function () {
        $(this).attr('title', function () {
            return $(this).attr('elem-name');
        })
    }, function () {
        $(this).removeAttr('title');
    });

    var $asideBox = $('#aside-main-box'),
        $ul = $('<ul>').attr('class', 'list-group');
    $('.named-content.geo-json').each(function () {
        var coordinates = JSON.parse($(this).attr('specific-use'))['coordinates'];
        $('<li>').attr('class', 'list-group-item').text(coordinates.toString()).appendTo($ul);
    });

    $('<div>').attr('class', 'panel-heading').text('Coordinates').appendTo($asideBox);
    $('<div>').attr('class', 'panel-body').append($ul).appendTo($asideBox);

}(jQuery))