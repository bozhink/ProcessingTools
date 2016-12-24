/* globals angular */

var Reporter = require('../services/message-reporter'),
    DataSet = require('../data/data-set'),
    NgJsonRequester = require('../services/ng-json-requester'),
    SearchStringService = require('../services/search-string-service'),
    Router = require('../routers/bio-data-router'),
    NavigationTabsDirective = require('../directives/navigation-tabs').navigationTabs,
    TaxaRanksDirective = require('../directives/taxa-ranks').taxaRanks,
    TaxaRanksController = require('../controllers/data/taxa-ranks-controller'),
    BiotaxonomicBlackListController = require('../controllers/data/biotaxonomic-black-list-controller'),
    NavigationController = require('../controllers/nav-controller');

angular.module('bioDataApp', ['ng', 'ngRoute'])
    .config(['$httpProvider', 'UrlMap', function ($httpProvider, UrlMap) {
        $httpProvider.interceptors.push(function () {
            return {
                'request': function (config) {
                    var url = config.url;
                    config.url = UrlMap[url] || url;
                    return config;
                }
            };
        });
    }])
    .config(['$routeProvider', Router])
    .constant('Pages', [
        {
            title: 'Home',
            route: '/'
        },
        {
            title: 'Taxa Ranks',
            route: '/taxa-ranks'
        },
        {
            title: 'Black List',
            route: '/black-list'
        }
    ])
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
    .directive('navigationTabs', [
        NavigationTabsDirective
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
    ])
    .controller('NavigationController', [
        '$location',
        'Pages',
        NavigationController
    ]);