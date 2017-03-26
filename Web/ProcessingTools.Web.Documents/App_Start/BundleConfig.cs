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
            bundles.Add(new ScriptBundle(BundleNames.DefaultScripts)
                .Include(
                    "~/bower_components/jquery/dist/jquery.js",
                    "~/bower_components/tether/dist/js/tether.min.js",
                    "~/bower_components/bootstrap/dist/js/bootstrap.js",
                    "~/bower_components/respond/src/*.js"));

            bundles.Add(new StyleBundle(BundleNames.DefaultStyles)
                .Include(
                    "~/bower_components/tether/dist/css/tether.min.css",
                    "~/bower_components/bootstrap/dist/css/bootstrap.css",
                    "~/bower_components/font-awesome/css/font-awesome.min.css",
                    "~/wwwroot/build/dist/css/site.css",
                    "~/wwwroot/build/dist/css/tooltips.css"));

            // Angular
            bundles.Add(new ScriptBundle(BundleNames.AngularScripts)
                .Include(
                    "~/bower_components/angular/angular.min.js"));

            // JQuery
            bundles.Add(new ScriptBundle(BundleNames.JQueryValidationScripts)
                .Include(
                    "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                    "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"));

            // Modernizr
            bundles.Add(new ScriptBundle(BundleNames.ModernizrScripts)
                .Include(
                    "~/bower_components/modernizr/modernizr.js"));

            // Files Index
            bundles.Add(new ScriptBundle(BundleNames.FilesIndexScripts)
                .Include(
                    "~/wwwroot/build/dist/js/apps/files-index.min.js"));

            // Address List
            bundles.Add(new ScriptBundle(BundleNames.AddressListScripts)
                .Include(
                    "~/wwwroot/build/dist/js/apps/controllers/address-list-controller.js"));

            bundles.Add(new StyleBundle(BundleNames.AddressListStyles)
                .Include(
                    "~/wwwroot/build/dist/css/address-list.css"));

#if Release
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
