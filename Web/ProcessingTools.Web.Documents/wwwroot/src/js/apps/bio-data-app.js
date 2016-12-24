/* globals angular */

var DataSet = require('../data/data-set');

var app = angular.module('bioDataApp', [])
    .service('DataSet', DataSet);

