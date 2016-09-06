(function (angular, app) {
    'use strict';

    if (angular.version.major > 1) {
        throw 'Angular 1 is needed for this application';
    }

    angular.module('taxaranksApp', [])
        .factory('DataSet', [
            app.data.DataSet
        ])
        .service('SearchStringService', [
            '$http',
            app.services.SearchStringService
        ])
        .controller('TaxaRanksController', [
            'DataSet',
            'SearchStringService',
            app.controllers.TaxaRanksController
        ]);
}(window.angular, window.app));
