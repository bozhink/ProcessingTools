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

export function TaxaRanksDirective(): ng.IDirective {
    return {
        controller: "TaxaRanksController as taxaList",
        templateUrl: "taxa-ranks.tpl.html"
    };
}
