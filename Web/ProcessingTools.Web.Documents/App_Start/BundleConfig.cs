namespace ProcessingTools.Web.Documents
{
    using System.Web.Optimization;
    using ProcessingTools.Web.Common.Constants;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Angular
            bundles.Add(new ScriptBundle(BundleNames.AngularScriptBundleName)
                .Include(
                    "~/bower_components/angular/angular.min.js"));

            // JQuery
            bundles.Add(new ScriptBundle(BundleNames.JQueryScriptBundleName)
                .Include(
                    "~/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle(BundleNames.JQueryFormScriptBundleName)
                .Include(
                    "~/bower_components/jquery-form/jquery.form.js"));

            bundles.Add(new ScriptBundle(BundleNames.JQueryValidationScriptBundleName)
                .Include(
                    "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                    "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"));

            // CryptoJS
            bundles.Add(new ScriptBundle(BundleNames.CryptoJSScriptBundleName)
                .Include(
                    "~/bower_components/cryptojs-sha1/cryptojs-sha1.js"));

            // InteractJS
            bundles.Add(new ScriptBundle(BundleNames.InteractJSScriptBundleName)
                .Include(
                    "~/bower_components/interact/interact.js",
                    "~/wwwroot/build/dist/js/app/configurations/interact-config.js"));

            // Modernizr
            bundles.Add(new ScriptBundle(BundleNames.ModernizrScriptBundleName)
                .Include(
                    "~/bower_components/modernizr/modernizr.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle(BundleNames.BootstrapScriptBundleName)
                .Include(
                    "~/bower_components/bootstrap/dist/js/bootstrap.js",
                    "~/bower_components/respond/src/*.js"));

            // Toastr
            bundles.Add(new ScriptBundle(BundleNames.ToastrScriptBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.js",
                    "~/wwwroot/build/dist/js/app/configurations/toastr-config.js"));

            // Handlebars
            bundles.Add(new ScriptBundle(BundleNames.HandlebarsScriptBundleName)
                .Include(
                    "~/bower_components/handlebars/handlebars.min.js"));

            // Leaflet
            bundles.Add(new ScriptBundle(BundleNames.LeafletScriptBundleName)
                .Include(
                    "~/bower_components/leaflet/dist/leaflet.js"));

            bundles.Add(new StyleBundle(BundleNames.LeafletStyleBundleName)
                .Include(
                    "~/bower_components/leaflet/dist/leaflet.css"));

            // MonacoEditor
            bundles.Add(new ScriptBundle(BundleNames.MonacoEditorScriptBundleName)
                .Include(
                    "~/node_modules/monaco-editor/min/vs/loader.js",
                    "~/wwwroot/build/dist/js/app/configurations/monaco-editor-config.js"));

            // Custom
            bundles.Add(new StyleBundle(BundleNames.DefaultStyleBundleName)
                .Include(
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/wwwroot/build/dist/css/site.css",
                    "~/wwwroot/build/dist/css/tooltips.css"));

            bundles.Add(new ScriptBundle(BundleNames.ApplicationScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/app/services/json-requester.js",
                    "~/wwwroot/build/dist/js/app/services/templates-provider.js"));

            bundles.Add(new ScriptBundle(BundleNames.KeyBindingsScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/custom/key-bindings.js"));

            bundles.Add(new ScriptBundle(BundleNames.AutoSaveScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/custom/auto-save.js"));

            // Files Index
            bundles.Add(new ScriptBundle(BundleNames.FilesIndexScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/custom/files-index.js"));

            // Document Edit
            bundles.Add(new ScriptBundle(BundleNames.DocumentEditScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/app/services/documents/document-content-data.js",
                    "~/wwwroot/build/dist/js/app/controllers/documents/document-controller.js",
                    "~/wwwroot/build/dist/js/custom/document-edit.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentEditStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/wwwroot/build/dist/css/document-edit.css"));

            // Document Preview
            bundles.Add(new ScriptBundle(BundleNames.DocumentPreviewScriptBundleName)
                .Include(
                    "~/wwwroot/build/dist/js/app/services/documents/document-content-data.js",
                    "~/wwwroot/build/dist/js/app/controllers/documents/document-controller.js",
                    "~/wwwroot/build/dist/js/app/services/templates-provider.js",
                    "~/wwwroot/build/dist/js/custom/html-selection-tagger.js",
                    "~/wwwroot/build/dist/js/custom/toolbox-event-handlers.js",
                    "~/wwwroot/build/dist/js/custom/coordinates-toolboxes.js",
                    "~/wwwroot/build/dist/js/custom/document-preview.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentPreviewStyleBundleName)
                .Include(
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

            ////BundleTable.EnableOptimizations = true;
        }
    }
}
