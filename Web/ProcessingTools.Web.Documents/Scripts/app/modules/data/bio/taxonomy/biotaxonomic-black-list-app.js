(function (angular, app) {
    'use strict';

    if (angular.version.major > 1) {
        throw 'Angular 1 is needed for this application';
    }

    angular.module('biotaxonomicblacklistApp', [])
        .service('DataSet', [
            app.data.DataSet
        ])
        .factory('NgJsonRequester', [
            '$http',
            app.services.NgJsonRequester
        ])
        .factory('SearchStringService', [
            'NgJsonRequester',
            app.services.SearchStringService
        ])
        .controller('BiotaxonomicBlackListController', [
            'DataSet',
            'SearchStringService',
            app.controllers.BiotaxonomicBlackListController
        ]);
}(window.angular, window.app));
