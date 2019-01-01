(function (window, document, $) {
    'use strict';

    $('nav.edit-buttons')
        .on('click', '#btn-save', function (event) {
            var e = event || window.event, $form = $(e.target).parents('form');
            e.stopPropagation();
            e.preventDefault();
            if ($form) {
                $('input[type=hidden][name=exit]').val(false);
                $('input[type=hidden][name=createNew]').val(false);
                $('input[type=hidden][name=cancel]').val(false);
                $form.submit();
            }
        })
        .on('click', '#btn-save-and-exit', function (event) {
            var e = event || window.event, $form = $(e.target).parents('form');
            e.stopPropagation();
            e.preventDefault();
            if ($form) {
                $('input[type=hidden][name=exit]').val(true);
                $('input[type=hidden][name=createNew]').val(false);
                $('input[type=hidden][name=cancel]').val(false);
                $form.submit();
            }
        })
        .on('click', '#btn-save-and-new', function (event) {
            var e = event || window.event, $form = $(e.target).parents('form');
            e.stopPropagation();
            e.preventDefault();
            if ($form) {
                $('input[type=hidden][name=exit]').val(false);
                $('input[type=hidden][name=createNew]').val(true);
                $('input[type=hidden][name=cancel]').val(false);
                $form.submit();
            }
        });
}(window, window.document, window.$));