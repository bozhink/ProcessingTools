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
                    "~/Scripts/config/interact-config.js"));

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
                    "~/Scripts/config/toastr-config.js"));

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
                    "~/Scripts/config/monaco-editor-config.js"));

            // Custom
            bundles.Add(new StyleBundle(BundleNames.DefaultStyleBundleName)
                .Include(
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/Content/site.min.css",
                    "~/Content/tooltips.min.css"));

            bundles.Add(new ScriptBundle(BundleNames.ApplicationScriptBundleName)
                .Include(
                    "~/Scripts/app/json-requester.js",
                    "~/Scripts/app/template.js"));

            bundles.Add(new ScriptBundle(BundleNames.KeyBindingsScriptBundleName)
                .Include(
                    "~/Scripts/custom/key-bindings.js"));

            bundles.Add(new ScriptBundle(BundleNames.AutoSaveScriptBundleName)
                .Include(
                    "~/Scripts/custom/auto-save.js"));

            // Files Index
            bundles.Add(new ScriptBundle(BundleNames.FilesIndexScriptBundleName)
                .Include(
                    "~/Scripts/custom/files-index.js"));

            // Document Edit
            bundles.Add(new ScriptBundle(BundleNames.DocumentEditScriptBundleName)
                .Include(
                    "~/Scripts/data/documents/document-content-data.js",
                    "~/Scripts/controllers/documents/document-controller.js",
                    "~/Scripts/custom/document-edit.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentEditStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/Content/document-edit.min.css"));

            // Document Preview
            bundles.Add(new ScriptBundle(BundleNames.DocumentPreviewScriptBundleName)
                .Include(
                    "~/Scripts/data/documents/document-content-data.js",
                    "~/Scripts/controllers/documents/document-controller.js",
                    "~/Scripts/app/template.js",
                    "~/Scripts/custom/html-selection-tagger.js",
                    "~/Scripts/custom/toolbox-event-handlers.js",
                    "~/Scripts/custom/coordinates-toolboxes.js",
                    "~/Scripts/custom/document-preview.js"));

            bundles.Add(new StyleBundle(BundleNames.DocumentPreviewStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/Content/toolbox.min.css",
                    "~/Content/document-preview.min.css"));

            // Address List
            bundles.Add(new ScriptBundle(BundleNames.AddressListScriptBundleName)
                .Include(
                    "~/Scripts/controllers/address-list-controller.js"));

            bundles.Add(new StyleBundle(BundleNames.AddressListStyleBundleName)
                .Include(
                    "~/Content/address-list.min.css"));

            // Taxa Ranks
            bundles.Add(new ScriptBundle(BundleNames.TaxaRanksScriptBundleName)
                .Include(
                    "~/Scripts/app/models/data/bio/taxonomy/taxon-rank.js",
                    "~/Scripts/app/data/data-set.js",
                    "~/Scripts/app/services/ng-json-requester.js",
                    "~/Scripts/app/services/search-string-service.js",
                    "~/Scripts/app/controllers/data/bio/taxonomy/taxa-ranks-controller.js",
                    "~/Scripts/app/modules/data/bio/taxonomy/taxa-ranks-app.js"));

            bundles.Add(new StyleBundle(BundleNames.TaxaRanksStyleBundleName)
                .Include(
                    "~/Content/taxa-ranks.min.css"));

            // Taxa Ranks
            bundles.Add(new ScriptBundle(BundleNames.BiotaxonomicBlackListScriptBundleName)
                .Include(
                    "~/Scripts/app/models/data/bio/taxonomy/black-list-item.js",
                    "~/Scripts/app/data/data-set.js",
                    "~/Scripts/app/services/ng-json-requester.js",
                    "~/Scripts/app/services/search-string-service.js",
                    "~/Scripts/app/controllers/data/bio/taxonomy/biotaxonomic-black-list-controller.js",
                    "~/Scripts/app/modules/data/bio/taxonomy/biotaxonomic-black-list-app.js"));

            bundles.Add(new StyleBundle(BundleNames.BiotaxonomicBlackListStyleBundleName)
                .Include(
                    "~/Content/black-list.min.css"));

            ////BundleTable.EnableOptimizations = true;
        }
    }
}
