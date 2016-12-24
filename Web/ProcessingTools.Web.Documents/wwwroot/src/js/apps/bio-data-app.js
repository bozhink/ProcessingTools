/* globals angular */

var Reporter = require('../services/message-reporter'),
    DataSet = require('../data/data-set'),
    NgJsonRequester = require('../services/ng-json-requester'),
    SearchStringService = require('../services/search-string-service'),
    TaxaRanksDirective = require('../directives/taxa-ranks').taxaRanks,
    TaxaRanksController = require('../controllers/data/taxa-ranks-controller'),
    BiotaxonomicBlackListController = require('../controllers/data/biotaxonomic-black-list-controller'),
    app = angular.module('bioDataApp', []);

app.config(['$httpProvider', function ($httpProvider) {

        var templateMap = {
            'taxa-ranks.tpl.html': '/wwwroot/build/dist/templates/taxa-ranks.tpl.html',
        };

        $httpProvider.interceptors.push(function () {
            return {
                'request': function (config) {
                    var url = config.url;
                    config.url = templateMap[url] || url;
                    return config;
                }
            };
        });
    }])
    .service('DataSet', [
        DataSet
    ])
    .factory('JsonRequester', [
        '$http',
        NgJsonRequester
    ])
    .factory('SearchStringService', [
        'JsonRequester',
        SearchStringService
    ])
    .factory('Reporter', [
        Reporter
    ])
    .directive('taxaRanks', [
        TaxaRanksDirective
    ])
    .controller('TaxaRanksController', [
        'DataSet',
        'SearchStringService',
        'JsonRequester',
        'Reporter',
        TaxaRanksController
    ])
    .controller('BiotaxonomicBlackListController', [
        'DataSet',
        'SearchStringService',
        'JsonRequester',
        'Reporter',
        BiotaxonomicBlackListController
    ]);