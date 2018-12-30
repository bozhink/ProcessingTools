export function NavigationTabsDirective(): ng.IDirective {
    return {
        controller: "NavigationController as nav",
        templateUrl: "navigation-tabs.tpl.html"
    };
}

export function BlackListDirective(): ng.IDirective {
    return {
        controller: "BlackListController as blackList",
        templateUrl: "black-list.tpl.html"
    };
}

export function TaxonRanksDirective(): ng.IDirective {
    return {
        controller: "TaxonRanksController as taxonRankList",
        templateUrl: "taxon-ranks.tpl.html"
    };
}
