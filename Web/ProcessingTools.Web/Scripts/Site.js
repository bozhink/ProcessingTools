(function (window, document, $) {
    'use strict';

    

    $('nav.edit-buttons')
        .on('click', '#btn-save', function (event) {
            var e = event || window.event, $form = $('form');
            e.stopPropagation();
            e.preventDefault();
            if ($form.length > 0) {
                $('input[type=hidden][name=exit]').val(false);
                $form[0].submit();
            }
        })
        .on('click', '#btn-save-and-exit', function (event) {
            var e = event || window.event, $form = $('form');
            e.stopPropagation();
            e.preventDefault();
            if ($form.length > 0) {
                $('input[type=hidden][name=exit]').val(true);
                $('input[type=hidden][name=createNew]').val(false);
                $('input[type=hidden][name=cancel]').val(false);
                $form[0].submit();
            }
        })
        .on('click', '#btn-save-and-new', function (event) {
            var e = event || window.event, $form = $('form');
            e.stopPropagation();
            e.preventDefault();
            if ($form.length > 0) {
                $('input[type=hidden][name=exit]').val(false);
                $('input[type=hidden][name=createNew]').val(true);
                $('input[type=hidden][name=cancel]').val(false);
                $form[0].submit();
            }
        });
}(window, window.document, window.$));