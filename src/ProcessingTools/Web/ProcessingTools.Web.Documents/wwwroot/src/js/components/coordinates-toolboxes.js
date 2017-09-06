'use strict';

module.exports = function CoordinatesToolboxes(window, $, leaflet, templatesProvider) {
    function clickMinimizeButtonEventHandler(event) {
        var e = event || window.event,
            target = e.target,
            toolbox,
            body;
        e.stopPropagation();
        e.preventDefault();

        if (target) {
            try {
                toolbox = target.parentNode.parentNode.parentNode;
                if (toolbox instanceof HTMLElement) {
                    body = toolbox.querySelector('.panel-body');
                }

                if (body instanceof HTMLElement) {
                    body.style.display = 'none';
                    toolbox.style.height = '60px';
                }
            } catch (ex) {
                // skip
            }
        }
    }

    function clickMaximizeButtonEventHandler(event) {
        var e = event || window.event,
            target = e.target,
            toolbox,
            body;
        e.stopPropagation();
        e.preventDefault();

        if (target) {
            try {
                toolbox = target.parentNode.parentNode.parentNode;
                if (toolbox instanceof HTMLElement) {
                    body = toolbox.querySelector('.panel-body');
                }

                if (body instanceof HTMLElement) {
                    body.style.display = 'block';
                    toolbox.style.height = '400px';
                }
            } catch (ex) {
                // skip
            }
        }
    }

    function clickCloseButtonEventHandler(event) {
        var e = event || window.event,
            target = e.target,
            toolbox;
        e.stopPropagation();
        e.preventDefault();

        if (target) {
            try {
                toolbox = target.parentNode.parentNode.parentNode;
                if (toolbox instanceof HTMLElement) {
                    toolbox.parentNode.removeChild(toolbox);
                    document.body.style.cursor = 'auto';
                }
            } catch (ex) {
                // skip
            }
        }
    }

    function listAnchorClickEventHandler(event) {
        const TEXT_TO_SCROLL_CLASS_NAME = 'selected-text-to-scroll';
        var e = event || window.event,
            target = e.target,
            href,
            $target;

        e.stopPropagation();
        e.preventDefault();

        if (!target) {
            return false;
        }

        if (target.classList.contains('coordinate-item')) {
            href = target.getAttribute('href');
            $target = $(href);

            $('html, body').animate({
                scrollTop: $target.offset().top - 250 + 'px'
            }, 'fast');

            $target.addClass(TEXT_TO_SCROLL_CLASS_NAME);
            setTimeout(function () {
                $target.removeClass(TEXT_TO_SCROLL_CLASS_NAME);
            }, 1500);

            return false;
        }
    }

    function getCoordinates() {
        var result = [];
        $('.named-content.geo-json').each(function (i, element) {
            var $that = $(element),
                id = $that.attr('id'),
                coordinates = JSON.parse($that.attr('specific-use')).coordinates;

            result.push({
                id: id,
                index: i,
                latitude: coordinates[1],
                longitude: coordinates[0]
            });
        });

        return result;
    }

    function genrateCoordinatesListToolbox(selector) {
        var toolboxSelector = '#coordinates-list-toolbox',
            $aside = $(selector),
            toolbox = {
                title: 'Coordinates',
                coordinates: getCoordinates()
            };

        // Remove all coordinates list toolboxes yet present.
        $(toolboxSelector).remove();

        templatesProvider.get('coordinates-toolbox')
            .then(function (template) {
                $('<div>')
                    .html(template(toolbox))
                    .appendTo($aside);
            })
            .then(function () {
                $(toolboxSelector + ' .panel-body .coordinates-list').on('click', listAnchorClickEventHandler);

                $(toolboxSelector + ' .minimize-button').on('click', clickMinimizeButtonEventHandler);
                $(toolboxSelector + ' .maximize-button').on('click', clickMaximizeButtonEventHandler);
                $(toolboxSelector + ' .close-button').on('click', clickCloseButtonEventHandler);
            });
    }

    function genrateCoordinatesMapToolbox(selector) {
        var toolboxSelector = '#coordinates-map-toolbox',
            $aside = $(selector),
            toolbox = {
                title: 'Map',
                coordinates: getCoordinates()
            };

        // Remove all coordinates list toolboxes yet present.
        $(toolboxSelector).remove();

        templatesProvider.get('coordinates-map')
            .then(function (template) {
                var i,
                    len,
                    map,
                    coordinate = [],
                    coordinates = toolbox.coordinates,
                    $div = $('<div>');

                // TODO: appendTo
                $div.html(template({
                    title: toolbox.title
                })).appendTo($aside);

                map = leaflet.map('coordinates-map', {
                    center: [0.0, 0.0],
                    zoom: 0,
                    worldCopyJump: true
                });

                leaflet.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                    attribution: '&copy; <a href="http://osm.org/copyright" target="_blank">OpenStreetMap</a> contributors'
                }).addTo(map);

                len = coordinates.length;
                for (i = 0; i < len; i += 1) {
                    coordinate = [coordinates[i].latitude, coordinates[i].longitude];
                    leaflet.marker(coordinate)
                        .bindPopup(`<a class="coordinate-item" href="#${coordinates[i].id}">${JSON.stringify(coordinate)}</a>`)
                        .addTo(map);
                }
            })
            .then(function () {
                $(toolboxSelector).on('click', '.coordinate-item', listAnchorClickEventHandler);

                $(toolboxSelector + ' .minimize-button').on('click', clickMinimizeButtonEventHandler);
                $(toolboxSelector + ' .maximize-button').on('click', clickMaximizeButtonEventHandler);
                $(toolboxSelector + ' .close-button').on('click', clickCloseButtonEventHandler);
            });
    }

    return {
        genrateCoordinatesListToolbox,
        genrateCoordinatesMapToolbox
    };
};
