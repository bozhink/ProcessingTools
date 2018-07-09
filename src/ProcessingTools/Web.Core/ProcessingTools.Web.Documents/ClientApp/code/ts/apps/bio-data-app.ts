import { MessageReporter as Reporter } from "../services/reporters/message-reporter";
import { DataSet as DataSet } from "../models/data.common.models";
import { NgJsonRequester as Requester } from "../services/http/ng-json-requester";
import { SearchStringService as SearchStringService } from "../services/search-string-service";
import { BioDataRouter as Router } from "../routers/data.bio.routers";
import { NavigationTabsDirective, TaxaRanksDirective, BlackListDirective } from "../directives/data.bio.directives";
import { NavigationController } from "../controllers/nav-controller";
import { BlackListController, TaxaRanksController } from "../controllers/data.bio.controllers";

declare let angular: ng.IAngularStatic;

angular.module("bioDataApp", ["ng", "ngRoute"])
    .config(["$httpProvider", "UrlMap", function ($httpProvider: ng.IHttpProvider, UrlMap: { [name: string]: string }): void {
        $httpProvider.interceptors.push(function (): any {
            return {
                "request": function (config: any): any {
                    let url: string = config.url;
                    config.url = UrlMap[url] || url;
                    return config;
                }
            };
        });
    }])
    .config(["$routeProvider", Router])
    .constant("Pages", [{
        title: "Home",
        route: "/"
    }, {
        title: "Taxa Ranks",
        route: "/taxa-ranks"
    }, {
        title: "Black List",
        route: "/black-list"
    }])
    .factory("TaxaRanksDataSet", [
        DataSet
    ])
    .factory("BlackListDataSet", [
        DataSet
    ])
    .service("JsonRequester", [
        "$http",
        Requester
    ])
    .service("SearchStringService", [
        "JsonRequester",
        SearchStringService
    ])
    .service("Reporter", [
        Reporter
    ])
    .directive("navigationTabs", [
        NavigationTabsDirective
    ])
    .directive("taxaRanks", [
        TaxaRanksDirective
    ])
    .directive("blackList", [
        BlackListDirective
    ])
    .controller("TaxaRanksController", [
        "TaxaRanksDataSet",
        "SearchStringService",
        "JsonRequester",
        "Reporter",
        TaxaRanksController
    ])
    .controller("BlackListController", [
        "BlackListDataSet",
        "SearchStringService",
        "JsonRequester",
        "Reporter",
        BlackListController
    ])
    .controller("NavigationController", [
        "$location",
        "Pages",
        NavigationController
    ]);
