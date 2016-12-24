/* globals angular */

var DataSet = require('../data/data-set'),
    JsonRequester = require('../services/ng-json-requester');

var app = angular.module('bioDataApp', [])
    .service('DataSet', [
        DataSet
    ])
    .factory('JsonRequester', [
        '$http',
        JsonRequester
    ]);