/* globals angular */

var DataSet = require('../data/data-set'),
    JsonRequester = require('../services/ng-json-requester'),
    SearchStringService = require('../services/search-string-service');

var app = angular.module('bioDataApp', [])
    .service('DataSet', [
        DataSet
    ])
    .factory('JsonRequester', [
        '$http',
        JsonRequester
    ])
    .factory('SearchStringService', [
        'JsonRequester',
        SearchStringService
    ]);