namespace ProcessingTools.Web.Documents
{
    using System.Web.Optimization;
    using ProcessingTools.Web.Common.Constants;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles
                .Add(new ScriptBundle(BundleNames.JQueryScriptBundleName)
                .Include(
                    "~/bower_components/jquery/dist/jquery.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.JQueryFormScriptBundleName)
                .Include(
                    "~/bower_components/jquery-form/jquery.form.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.JQueryValidationScriptBundleName)
                .Include(
                    "~/Scripts/jquery.validate*"));

            bundles
                .Add(new ScriptBundle(BundleNames.CryptoJSScriptBundleName)
                .Include(
                    "~/bower_components/cryptojs-sha1/cryptojs-sha1.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.InteractJSScriptBundleName)
                .Include(
                    "~/bower_components/interact/interact.js",
                    "~/Scripts/custom/interact-config.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.ModernizrScriptBundleName)
                .Include(
                    "~/bower_components/modernizr/modernizr.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.BootstrapScriptBundleName)
                .Include(
                    "~/bower_components/bootstrap/dist/js/bootstrap.js",
                    "~/bower_components/respond/src/*.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.ToastrScriptBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.js",
                    "~/Scripts/custom/toastr-config.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.NetRequesterScriptBundleName)
                .Include(
                    "~/Scripts/custom/json-requester.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.MonacoEditorScriptBundleName)
                .Include(
                    "~/node_modules/monaco-editor/min/vs/loader.js",
                    "~/Scripts/custom/monaco-editor-config.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.KeyBindingsScriptBundleName)
                .Include(
                    "~/Scripts/custom/key-bindings.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.AutoSaveScriptBundleName)
                .Include(
                    "~/Scripts/custom/auto-save.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.DocumentEditScriptBundleName)
                .Include(
                    "~/Scripts/custom/document-save-controller.js",
                    "~/Scripts/custom/document-edit.js"));

            bundles
                .Add(new ScriptBundle(BundleNames.DocumentPreviewScriptBundleName)
                .Include(
                    "~/Scripts/custom/document-save-controller.js",
                    "~/Scripts/custom/document-preview.js"));

            bundles
                .Add(new StyleBundle(BundleNames.DefaultStyleBundleName)
                .Include(
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/Content/site.min.css",
                    "~/Content/tooltips.min.css"));

            bundles
                .Add(new StyleBundle(BundleNames.DocumentPreviewStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/Content/toolbox.min.css",
                    "~/Content/documentPreview.min.css"));

            bundles
                .Add(new StyleBundle(BundleNames.DocumentEditStyleBundleName)
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/Content/documentEdit.min.css"));

            ////BundleTable.EnableOptimizations = true;
        }
    }
}
