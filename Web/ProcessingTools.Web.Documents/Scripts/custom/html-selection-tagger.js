(function (window, document) {
    'use strict';

    window.htmlSelectionTagger = (function () {
        function foo() {
            var selection = window.getSelection().getRangeAt(0),
                selectedText = selection.extractContents(),
                span = document.createElement("span");
            span.style.backgroundColor = "yellow";
            span.appendChild(selectedText);
            selection.insertNode(span);
        }

        function clearTagsInSelection() {
            var selection = window.getSelection().getRangeAt(0),
                selectedText = selection.extractContents(),
                span = document.createElement("span");
            span.appendChild(selectedText);

            span.innerHTML = span.innerHTML.replace(/<\/?[^!<>]+>/g, '');
            span.setAttribute('class', 'cleaned-selection');

            selection.insertNode(span);
        }

        function tag(tagName, elemName, className, attributes, callback) {
            var i,
                len,
                attribute,
                range,
                selectedText,
                tagElement,
                selection = window.getSelection();

            len = selection.rangeCount;
            for (i = 0; i < len; i += 1) {
                tagElement = document.createElement(tagName);
                tagElement.setAttribute('elem-name', elemName);

                if (!className) {
                    tagElement.setAttribute('class', elemName);
                } else {
                    tagElement.setAttribute('class', className);
                }

                if (attributes) {
                    for (attribute in attributes) {
                        tagElement.setAttribute(attribute, attributes[attribute]);
                    }
                }

                range = selection.getRangeAt(i);
                selectedText = range.extractContents();
                tagElement.appendChild(selectedText);

                if (callback) {
                    callback(tagElement);
                }

                range.insertNode(tagElement);
            }
        }

        function tagLink() {
            tag('a', 'ext-link', 'ext-link', {
                'target': '_blank',
                'type': 'simple',
                'xlink:type': 'simple',
                'xmlns:xlink': 'http://www.w3.org/1999/xlink',
                'ext-link-type': 'uri'
            }, function (tagElement) {
                var href = tagElement.innerText.trim();
                tagElement.setAttribute('href', href);
                tagElement.setAttribute('xlink:href', href);
            });
        }

        function tagInSpan(elemName, className) {
            tag('span', elemName, className);
        }

        function tagInMark(elemName, className) {
            tag('mark', elemName, className);
        }

        function tagInXref(rid, refType) {
            var elemName = 'xref',
                className = elemName + ' ' + refType,
                attributes = {
                    'rid': rid,
                    'ref-type': refType,
                    'href': '#' + rid
                };

            tag('a', elemName, className, attributes);
        }

        function tagInBold() {
            tag('b', 'bold', 'bold');
        }

        function tagInItalic() {
            tag('i', 'italic', 'italic');
        }

        function tagInUnderline() {
            tag('u', 'underline', 'underline');
        }

        return {
            foo,
            clearTagsInSelection,
            tag,
            tagLink,
            tagInSpan,
            tagInMark,
            tagInXref,
            tagInBold,
            tagInItalic,
            tagInUnderline
        };
    }());
}(window, document));