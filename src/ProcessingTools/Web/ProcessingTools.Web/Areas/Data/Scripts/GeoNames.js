(function (window, $) {
    'use strict';

    $('#deleteModal').on('show.bs.modal', function (event) {
        var $button = $(event.relatedTarget),
            $data = $button.parents('.grid-row').find('.data-content'),
            $modal = $(this);

        $modal.find('input[name=id]').val($data.attr('data-id'));
        $modal.find('.modal-body .deleteModal-content').html($data.html());
    });

    $('#detailsModal').on('show.bs.modal', function (event) {
        var $button = $(event.relatedTarget),
            $data = $button.parents('.grid-row').find('.data-content'),
            $modal = $(this);

        $modal.find('.modal-body .detailsModal-content .details-name').html($data.html());
    });

    $('#editModal').on('show.bs.modal', function (event) {
        var $button = $(event.relatedTarget),
            $data = $button.parents('.grid-row').find('.data-content'),
            $modal = $(this);

        $modal.find('input[name=id]').val($data.attr('data-id'));
        $modal.find('input[name=Name]').val($data.html());
    });

    $('#createModal').on('show.bs.modal', function (event) {
        var $button = $(event.relatedTarget),
            $modal = $(this);

        $modal.find('textarea').val('');
    });

}(window, window.$))