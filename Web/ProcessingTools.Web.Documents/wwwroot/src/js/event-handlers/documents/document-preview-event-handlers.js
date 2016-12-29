const MAIN_ASIDE_ID = 'aside-main-box';

module.exports = function (window, document, $, factory, tagger, coordinatesToolboxes) {


    function createBaloon(event, contentSelector) {
        var e = event || window.event,
            rid = e.target.getAttribute('href'),
            $contentNode = $(rid),
            $aside = $('#' + MAIN_ASIDE_ID),
            $baloon,
            content;

        contentSelector = contentSelector || '';
        if (contentSelector === '') {
            content = $contentNode.text();
        } else {
            content = $contentNode.find(contentSelector).text();
        }

        $aside.find('.balloon').remove();

        $baloon = $('<div>')
            .attr('role', 'balloon')
            .addClass('balloon')
            .text(content)
            .css({
                'top': (e.clientY + 10) + 'px',
                'left': (e.clientX - 20) + 'px'
            });

        $baloon.appendTo($aside);
    }

    function removeBalloon() {
        var $aside = $('#' + MAIN_ASIDE_ID);
        $aside.find('.balloon').remove();
    }

    function manualTagMenufactory(menuName, callback) {
        var $aside = $('#' + MAIN_ASIDE_ID),
            $supermenu = $('#supermenu'),
            $menu;

        // Remove notification
        $('#manual-mode-notifier').remove();
        $('<div>')
            .addClass('mode-notifier')
            .attr('id', 'manual-mode-notifier')
            .text(menuName)
            .appendTo($aside);

        // Create menu
        $('.manual-tag-menu').remove();
        $menu = $('<menu>')
            .addClass('manual-tag-menu')
            .attr('id', 'menu-bibliographic-citations')
            .attr('label', menuName);

        // Populate the menu
        callback($menu);

        // Attach the menu to DOM
        $menu.appendTo($supermenu);
    }

    return {
        tagBibliographicCitation: factory.create(function (e) {
            var rid = e.target.getAttribute('rid');
            tagger.tagInXref(rid, 'bibr');
        }),
        tagAppendicesCitation: factory.create(function (e) {
            var rid = e.target.getAttribute('rid');
            tagger.tagInXref(rid, 'app');
        }),
        tagSupplMaterialsCitation: factory.create(function (e) {
            var rid = e.target.getAttribute('rid');
            tagger.tagInXref(rid, 'supplementary-material');
        }),
        tagTablesCitation: factory.create(function (e) {
            var rid = e.target.getAttribute('rid');
            tagger.tagInXref(rid, 'table');
        }),
        tagFiguresCitation: factory.create(function (e) {
            var rid = e.target.getAttribute('rid');
            tagger.tagInXref(rid, 'fig');
        }),
        emailThisPage: factory.create(function () {
            window.location = 'mailto:?body=' + window.location.href;
        }),
        foo: factory.create(tagger.foo),
        tagLink: factory.create(function () {
            tagger.tagLink();
        }),
        tagCoordinate: factory.create(function () {
            tagger.tagInSpan('locality-coordinates');
        }),
        tagAbbrev: factory.create(function () {
            tagger.tag('abbr', 'abbrev');
        }),
        tagAbbrevDef: factory.create(function () {
            tagger.tagInSpan('p');
            tagger.tagInSpan('def');
        }),
        tagbibliographyElement: factory.create(function (e) {
            var elementName = e.target.id.toString().substr(10);
            tagger.tagInMark(elementName);
        }),
        tagBibliographicCitationMenuClick: factory.create(function (e) {
            var $target = $(e.target);
            manualTagMenufactory($target.text(), function ($menu) {
                $('.ref').each(function (i, element) {
                    var $element = $(element);
                    $('<menuitem>')
                        .addClass('mi-bibr')
                        .attr('id', 'ref-' + i)
                        .attr('rid', $element.attr('id'))
                        .attr('label', $element.text().trim())
                        .appendTo($menu);
                });
            });
        }),
        tagAppendicesCitationMenuClick: factory.create(function (e) {
            var $target = $(e.target);
            manualTagMenufactory($target.text(), function ($menu) {
                $('.app').each(function (i, element) {
                    var $element = $(element);
                    $('<menuitem>')
                        .addClass('mi-app')
                        .attr('id', 'app-' + i)
                        .attr('rid', $element.attr('id'))
                        .attr('label', $element.find('.title').text().trim())
                        .appendTo($menu);
                });
            });
        }),
        tagSupplMaterialsCitationMenuClick: factory.create(function (e) {
            var $target = $(e.target);
            manualTagMenufactory($target.text(), function ($menu) {
                $('.supplementary-material').each(function (i, element) {
                    var $element = $(element);
                    $('<menuitem>')
                        .addClass('mi-suppl-material')
                        .attr('id', 'suppl-material-' + i)
                        .attr('rid', $element.attr('id'))
                        .attr('label', $element.find('.label').text().trim())
                        .appendTo($menu);
                });
            });
        }),
        tagTablesCitationMenuClick: factory.create(function (e) {
            var $target = $(e.target);
            manualTagMenufactory($target.text(), function ($menu) {
                $('.table-wrap').each(function (i, element) {
                    var $element = $(element);
                    $('<menuitem>')
                        .addClass('mi-tab')
                        .attr('id', 'tab-' + i)
                        .attr('rid', $element.attr('id'))
                        .attr('label', $element.find('.label').text().trim())
                        .appendTo($menu);
                });
            });
        }),
        tagFiguresCitationMenuClick: factory.create(function (e) {
            var $target = $(e.target);
            manualTagMenufactory($target.text(), function ($menu) {
                $('.fig').each(function (i, element) {
                    var $element = $(element);
                    $('<menuitem>')
                        .addClass('mi-fig')
                        .attr('id', 'fig-' + i)
                        .attr('rid', $element.attr('id'))
                        .attr('label', $element.find('.label').text().trim())
                        .appendTo($menu);
                });
            });
        }),
        setElementInEditMode: function (event) {
            var e = event || window.event,
                $target = $(e.target),
                name = $target.prop('nodeName').toLowerCase();

            if (name === 'p' || name === 'td' || name === 'th') {
                e.stopPropagation();
                e.preventDefault();
                $target
                    .attr('contenteditable', '')
                    .addClass('in-edit');
            }
        },
        unsetElementInEditMode: function (event) {
            var e = event || window.event,
                $target = $(e.target),
                name = $target.prop('nodeName').toLowerCase();

            if (name === 'p' || name === 'td' || name === 'th') {
                e.stopPropagation();
                e.preventDefault();
                $target
                    .removeAttr('contenteditable')
                    .removeClass('in-edit');
            }
        },
        unsetAllInEditMode: function (event) {
            var e = event || window.event;
            e.stopPropagation();
            e.preventDefault();
            $('.in-edit')
                .removeAttr('contenteditable')
                .removeClass('in-edit');
        },
        clearTagsInSelection: factory.create(tagger.clearTagsInSelection),
        tagInBold: factory.create(tagger.tagInBold),
        tagInItalic: factory.create(tagger.tagInItalic),
        tagInUnderline: factory.create(tagger.tagInUnderline),
        tagInMonospace: factory.create(tagger.tagInMonospace),
        mouseoverXref: function (event) {
            var e = event || window.event,
                target = e.target;

            if (target.classList.contains('xref')) {
                e.stopPropagation();
                e.preventDefault();

                if (target.classList.contains('bibr')) {
                    createBaloon(e);
                }

                if (target.classList.contains('aff')) {
                    createBaloon(e, '.addr-line');
                }

                if (target.classList.contains('fig')) {
                    createBaloon(e, '.caption');
                }

                if (target.classList.contains('table')) {
                    createBaloon(e, '.caption');
                }

                return false;
            }
        },
        mouseoutXref: function (event) {
            var e = event || window.event,
                target = e.target;
            if (target.classList.contains('xref')) {
                e.stopPropagation();
                e.preventDefault();

                removeBalloon();

                return false;
            }
        },
        genrateCoordinatesListToolbox: factory.create(function () {
            coordinatesToolboxes.genrateCoordinatesListToolbox('#' + MAIN_ASIDE_ID);
        }),
        genrateCoordinatesMapToolbox: factory.create(function () {
            coordinatesToolboxes.genrateCoordinatesMapToolbox('#' + MAIN_ASIDE_ID);
        })
    };
}