(function () {
    toastr.options.closeMethod = 'fadeOut';
    toastr.options.closeDuration = 2000;

    toastr.options.showEasing = 'swing';
    toastr.options.hideEasing = 'linear';
    toastr.options.closeEasing = 'linear';

    toastr.options.timeOut = 2000; // How long the toast will display without user interaction
    toastr.options.extendedTimeOut = 3000; // How long the toast will display after a user hovers over it

    toastr.options.progressBar = true;
}())