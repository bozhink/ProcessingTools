/* globals angular */

var Reporter = require('../services/message-reporter'),
    DataSet = require('../data/data-set'),
    NgJsonRequester = require('../services/ng-json-requester'),
    SearchStringService = require('../services/search-string-service'),
    TaxaRanksController = require('../controllers/data/taxa-ranks-controller'),
    BiotaxonomicBlackListController = require('../controllers/data/biotaxonomic-black-list-controller');

var app = angular.module('bioDataApp', [])
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