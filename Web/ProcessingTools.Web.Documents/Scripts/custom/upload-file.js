(function () {
    var bar = $('.progress-bar'),
        percent = $('.progress-bar'),
        status = $('#status');

    $('form').ajaxForm({
        beforeSend: function () {
            var percentVal = '0%';
            status.empty();
            bar.width(percentVal)
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + '%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        success: function () {
            var percentVal = '100%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        complete: function (xhr) {
            status.html(xhr.responseText);
        }
    });
}());
