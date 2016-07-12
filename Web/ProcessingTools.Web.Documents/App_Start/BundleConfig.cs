namespace ProcessingTools.Web.Documents
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryfrom")
                .Include(
                    "~/bower_components/jquery-form/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include(
                    "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include(
                    "~/bower_components/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include(
                    "~/bower_components/bootstrap/dist/js/bootstrap.js",
                    "~/bower_components/respond/src/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/documentPreview")
                .Include(
                    "~/Scripts/custom/documentPreview.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/Content/site.min.css",
                    "~/Content/tooltips.min.css"));

            bundles.Add(new StyleBundle("~/Content/documentPreview")
                .Include(
                    "~/Content/toolbox.min.css",
                    "~/Content/documentPreview.min.css"));

            bundles.Add(new StyleBundle("~/Content/documentEdit")
                .Include(
                    "~/bower_components/toastr/toastr.min.css",
                    "~/Content/documentEdit.min.css"));

            ////BundleTable.EnableOptimizations = true;
        }
    }
}