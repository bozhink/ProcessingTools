(function (window, $) {
    'use strict';

    $('#deleteModal').on('show.bs.modal', function (event) {
        var $button = $(event.relatedTarget),
            $data = $button.parents('.grid-row').find('.data-content'),
            $modal = $(this);

        $modal.find('input[name=id]').val($data.attr('data-id'));
        $modal.find('.modal-body .deleteModal-content').html($data.html());
    });

}(window, window.$))