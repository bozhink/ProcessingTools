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
                    "~/static/code/app/configurations/interact-config.js"));

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
                    "~/static/code/app/configurations/toastr-config.js"));

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
                    "~/static/code/app/configurations/monaco-editor-config.js"));

            // Custom
            bundles.Add(new StyleBundle(BundleNames.DefaultStyleBundleName)
                .Include(
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/static/styles/site.min.css",
                    "~/static/styles/tooltips.min.css"));

            bundles.Add(new ScriptBundle(BundleNames.ApplicationScriptBundleName)
                .Include(
                    "~/static/code/app/services/json-requester.js",
                    "~/static/code/app/services/templates-provider.js"));

            bundles.Add(new ScriptBundle(BundleNames.KeyBindingsScriptBundleName)
                .Include(
                    "~/static/code/custom/key-bindings.js"));

            bundles.Add(new ScriptBundle(BundleNames.AutoSaveScriptBundleName)
                .Include(
                    "~/static/code/custom/auto-save.js"));

            // Files Index
            bundles.Add(new ScriptBundle(BundleNames.FilesIndexScriptBundleName)
                .Include(
                    "~/static/code/custom/files-index.js"));

            // Document Edit
            bundles.Add(new ScriptBundle(BundleNames.DocumentEditScriptBundleName)
                .Include(
                    "~/static/code/app/services/documents/document-content-data.js",
                    "~/static/code/app/controllers/documents/document-controller.js",
                    "~/static/code/custom/document-edit.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentEditStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/static/styles/document-edit.min.css"));

            // Document Preview
            bundles.Add(new ScriptBundle(BundleNames.DocumentPreviewScriptBundleName)
                .Include(
                    "~/static/code/app/services/documents/document-content-data.js",
                    "~/static/code/app/controllers/documents/document-controller.js",
                    "~/static/code/app/services/templates-provider.js",
                    "~/static/code/custom/html-selection-tagger.js",
                    "~/static/code/custom/toolbox-event-handlers.js",
                    "~/static/code/custom/coordinates-toolboxes.js",
                    "~/static/code/custom/document-preview.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentPreviewStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/static/styles/toolbox.min.css",
                    "~/static/styles/document-preview.min.css"));

            // Address List
            bundles.Add(new ScriptBundle(BundleNames.AddressListScriptBundleName)
                .Include(
                    "~/static/code/app/controllers/address-list-controller.js"));

            bundles.Add(new StyleBundle(BundleNames.AddressListStyleBundleName)
                .Include(
                    "~/static/styles/address-list.min.css"));

            // Taxa Ranks
            bundles.Add(new ScriptBundle(BundleNames.TaxaRanksScriptBundleName)
                .Include(
                    "~/static/code/app/models/data/bio/taxonomy/taxon-rank.js",
                    "~/static/code/app/data/data-set.js",
                    "~/static/code/app/services/ng-json-requester.js",
                    "~/static/code/app/services/search-string-service.js",
                    "~/static/code/app/controllers/data/bio/taxonomy/taxa-ranks-controller.js",
                    "~/static/code/app/modules/data/bio/taxonomy/taxa-ranks-app.js"));

            bundles.Add(new StyleBundle(BundleNames.TaxaRanksStyleBundleName)
                .Include(
                    "~/static/styles/biotaxonomic-lists.min.css"));

            // Biotaxonomic Black List
            bundles.Add(new ScriptBundle(BundleNames.BiotaxonomicBlackListScriptBundleName)
                .Include(
                    "~/static/code/app/models/data/bio/taxonomy/black-list-item.js",
                    "~/static/code/app/data/data-set.js",
                    "~/static/code/app/services/ng-json-requester.js",
                    "~/static/code/app/services/search-string-service.js",
                    "~/static/code/app/controllers/data/bio/taxonomy/biotaxonomic-black-list-controller.js",
                    "~/static/code/app/modules/data/bio/taxonomy/biotaxonomic-black-list-app.js"));

            bundles.Add(new StyleBundle(BundleNames.BiotaxonomicBlackListStyleBundleName)
                .Include(
                    "~/static/styles/biotaxonomic-lists.min.css"));

            ////BundleTable.EnableOptimizations = true;
        }
    }
}
