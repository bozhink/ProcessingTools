namespace ProcessingTools.Web.Documents
{
    using System.Web.Optimization;
    using ProcessingTools.Web.Common.Constants;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Defaults
            bundles.Add(new ScriptBundle(BundleNames.DefaultScriptBundleName)
                .Include(
                    "~/bower_components/jquery/dist/jquery.js",
                    "~/bower_components/tether/dist/js/tether.min.js",
                    "~/bower_components/bootstrap/dist/js/bootstrap.js",
                    "~/bower_components/respond/src/*.js"));

            bundles.Add(new StyleBundle(BundleNames.DefaultStyleBundleName)
                .Include(
                    "~/bower_components/tether/dist/css/tether.min.css",
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/bower_components/font-awesome/css/font-awesome.min.css",
                    "~/wwwroot/build/dist/css/site.css",
                    "~/wwwroot/build/dist/css/tooltips.css"));

            // Angular
            bundles.Add(new ScriptBundle(BundleNames.AngularScriptBundleName)
                .Include(
                    "~/bower_components/angular/angular.min.js"));

            // JQuery
            bundles.Add(new ScriptBundle(BundleNames.JQueryValidationScriptBundleName)
                .Include(
                    "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                    "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"));

            // Modernizr
            bundles.Add(new ScriptBundle(BundleNames.ModernizrScriptBundleName)
                .Include(
                    "~/bower_components/modernizr/modernizr.js"));

            // Files Index
            bundles.Add(new ScriptBundle(BundleNames.FilesIndexScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/apps/files-index.min.js"));

            // Document Edit
            bundles.Add(new ScriptBundle(BundleNames.DocumentEditScriptBundleName)
                .Include(
                    "~/bower_components/cryptojs-sha1/cryptojs-sha1.js",
                    "~/bower_components/toastr/toastr.js",
                    "~/node_modules/monaco-editor/min/vs/loader.js",
                    "~/wwwroot/build/dist/js/apps/document-edit.min.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentEditStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/wwwroot/build/dist/css/document-edit.css"));

            // Document Preview
            bundles.Add(new ScriptBundle(BundleNames.DocumentPreviewScriptBundleName)
                .Include(
                    "~/bower_components/cryptojs-sha1/cryptojs-sha1.js",
                    "~/bower_components/interact/interact.js",
                    "~/bower_components/handlebars/handlebars.min.js",
                    "~/bower_components/leaflet/dist/leaflet.js",
                    "~/bower_components/toastr/toastr.js",
                    "~/wwwroot/build/dist/js/apps/document-preview.min.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentPreviewStyleBundleName)
                .Include(
                    "~/bower_components/leaflet/dist/leaflet.css",
                    "~/bower_components/toastr/toastr.min.css",
                    "~/wwwroot/build/dist/css/toolbox.css",
                    "~/wwwroot/build/dist/css/document-preview.css"));

            // Address List
            bundles.Add(new ScriptBundle(BundleNames.AddressListScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/app/controllers/address-list-controller.js"));

            bundles.Add(new StyleBundle(BundleNames.AddressListStyleBundleName)
                .Include(
                    "~/wwwroot/build/dist/css/address-list.css"));

            // Bio Data App
            bundles.Add(new ScriptBundle(BundleNames.BioDataAppScriptBundleName)
                .Include(
                    "~/bower_components/angular/angular.min.js",
                    "~/wwwroot/build/dist/js/apps/bio-data-app.min.js"));

            // Taxa Ranks
            bundles.Add(new ScriptBundle(BundleNames.TaxaRanksScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/app/models/data/bio/taxonomy/taxon-rank.js",
                    "~/wwwroot/build/dist/js/app/data/data-set.js",
                    "~/wwwroot/build/dist/js/app/services/ng-json-requester.js",
                    "~/wwwroot/build/dist/js/app/services/search-string-service.js",
                    "~/wwwroot/build/dist/js/app/controllers/data/bio/taxonomy/taxa-ranks-controller.js",
                    "~/wwwroot/build/dist/js/app/modules/data/bio/taxonomy/taxa-ranks-app.js"));

            bundles.Add(new StyleBundle(BundleNames.TaxaRanksStyleBundleName)
                .Include(
                    "~/wwwroot/build/dist/css/biotaxonomic-lists.css"));

            // Biotaxonomic Black List
            bundles.Add(new ScriptBundle(BundleNames.BiotaxonomicBlackListScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/app/models/data/bio/taxonomy/black-list-item.js",
                    "~/wwwroot/build/dist/js/app/data/data-set.js",
                    "~/wwwroot/build/dist/js/app/services/ng-json-requester.js",
                    "~/wwwroot/build/dist/js/app/services/search-string-service.js",
                    "~/wwwroot/build/dist/js/app/controllers/data/bio/taxonomy/biotaxonomic-black-list-controller.js",
                    "~/wwwroot/build/dist/js/app/modules/data/bio/taxonomy/biotaxonomic-black-list-app.js"));

            bundles.Add(new StyleBundle(BundleNames.BiotaxonomicBlackListStyleBundleName)
                .Include(
                    "~/wwwroot/build/dist/css/biotaxonomic-lists.css"));
#if Release
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
