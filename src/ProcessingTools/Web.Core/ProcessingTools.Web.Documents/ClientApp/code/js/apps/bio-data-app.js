/* globals angular */

var Reporter = require('../services/reporters/message-reporter'),
    DataSet = require('../data/data-set'),
    NgJsonRequester = require('../services/http/ng-json-requester'),
    SearchStringService = require('../services/search-string-service'),
    Router = require('../routers/bio-data-router'),
    NavigationTabsDirective = require('../directives/navigation-tabs').navigationTabs,
    TaxaRanksDirective = require('../directives/taxa-ranks').taxaRanks,
    BlackListDirective = require('../directives/black-list').blackList,
    TaxaRanksController = require('../controllers/data/bio/taxa-ranks-controller'),
    BlackListController = require('../controllers/data/bio/black-list-controller'),
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
    .constant('Pages', [{
        title: 'Home',
        route: '/'
    }, {
        title: 'Taxa Ranks',
        route: '/taxa-ranks'
    }, {
        title: 'Black List',
        route: '/black-list'
    }])
    .factory('TaxaRanksDataSet', [
        DataSet
    ])
    .factory('BlackListDataSet', [
        DataSet
    ])
    .service('JsonRequester', [
        '$http',
        NgJsonRequester
    ])
    .service('SearchStringService', [
        'JsonRequester',
        SearchStringService
    ])
    .service('Reporter', [
        Reporter
    ])
    .directive('navigationTabs', [
        NavigationTabsDirective
    ])
    .directive('taxaRanks', [
        TaxaRanksDirective
    ])
    .directive('blackList', [
        BlackListDirective
    ])
    .controller('TaxaRanksController', [
        'TaxaRanksDataSet',
        'SearchStringService',
        'JsonRequester',
        'Reporter',
        TaxaRanksController
    ])
    .controller('BlackListController', [
        'BlackListDataSet',
        'SearchStringService',
        'JsonRequester',
        'Reporter',
        BlackListController
    ])
    .controller('NavigationController', [
        '$location',
        'Pages',
        NavigationController
    ]);
