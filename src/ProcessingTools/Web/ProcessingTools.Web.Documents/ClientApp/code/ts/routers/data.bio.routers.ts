export function BioDataRouter($routeProvider: ng.route.IRouteProvider): void {
    $routeProvider
        .when("/", {
            templateUrl: "bio-data-home.view.html"
        })
        .when("/taxon-ranks", {
            templateUrl: "bio-data-taxon-ranks.view.html"
        })
        .when("/black-list", {
            templateUrl: "bio-data-black-list.view.html"
        });
}
