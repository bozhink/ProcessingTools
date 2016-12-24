/* globals angular */

var DataSet = require('../data/data-set'),
    NgJsonRequester = require('../services/ng-json-requester'),
    SearchStringService = require('../services/search-string-service'),
    TaxaRanksController = require('../controllers/data/taxa-ranks-controller');

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
    .controller('TaxaRanksController', [
        'DataSet',
        'SearchStringService',
        'NgJsonRequester',
        TaxaRanksController
    ]);