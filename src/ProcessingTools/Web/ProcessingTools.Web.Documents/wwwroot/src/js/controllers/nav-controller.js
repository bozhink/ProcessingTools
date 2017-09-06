'use strict';

module.exports = function NavigationController($location, pages) {
    var self = this;

    if (!Array.isArray(pages)) {
        throw 'Invalid pages array';
    }

    self.pages = pages;

    self.activePage = '/';

    self.getActivePage = function () {
        var path = $location.path();
        self.activePage = path;
    }
};
