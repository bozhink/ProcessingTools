var articlePreviewActions = (function ($) {

    function toggleMarkTitle() {
        $('mark').hover(function () {
            $(this).attr('title', function () {
                return $(this).attr('elem-name');
            })
        }, function () {
            $(this).removeAttr('title');
        });
    }

    function genrateCoordinatesList(targetBoxId) {
        targetBoxId = '' + targetBoxId;
        if (targetBoxId.length < 1) {
            throw 'Invalid target box id!';
        }

        if (targetBoxId.substr(0, 1) !== '#') {
            targetBoxId = '#' + targetBoxId;
        }

        var $aside = $(targetBoxId),
            $section = $('<section>').addClass('panel').addClass('panel-default').attr('id', 'coordinates-list'),
            $list = $('<div>').addClass('list-group');

        $('.named-content.geo-json').each(function () {
            var $that = $(this),
                id = $that.attr('id'),
                coordinates = JSON.parse($that.attr('specific-use'))['coordinates'];

            $('<a>')
                .addClass('list-group-item')
                .attr('href', '#' + id)
                .text(coordinates.toString())
                .appendTo($list);
        });

        $('<div>')
            .addClass('panel-heading')
            .text('Coordinates')
            .appendTo($section);

        $('<div>')
            .addClass('panel-body')
            .append($list)
            .appendTo($section);

        $section.appendTo($aside);
    }

    return {
        toggleMarkTitle,
        genrateCoordinatesList
    }

}(jQuery));

articlePreviewActions.toggleMarkTitle();
articlePreviewActions.genrateCoordinatesList("#aside-main-box");