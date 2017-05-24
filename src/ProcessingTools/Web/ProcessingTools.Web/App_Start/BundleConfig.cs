namespace ProcessingTools.Web
{
    using System.Web.Optimization;
    using ProcessingTools.Constants.Web;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(BundleNames.JQueryScripts).Include(
                "~/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle(BundleNames.JQueryValidationScripts).Include(
                "~/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle(BundleNames.ModernizrScripts).Include(
                "~/bower_components/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle(BundleNames.KnockoutScripts).Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle(BundleNames.BootstrapScripts).Include(
                "~/bower_components/tether/dist/js/tether.min.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.js",
                "~/bower_components/respond/src/*.js"));

            bundles.Add(new ScriptBundle(BundleNames.DefaultScripts).Include(
                "~/Scripts/Site.js",
                "~/Scripts/JsonRequester.js"));

            bundles.Add(new StyleBundle(BundleNames.DefaultStyles).Include(
                "~/bower_components/tether/dist/css/tether.css",
                "~/bower_components/bootstrap/dist/css/bootstrap.css",
                "~/bower_components/font-awesome/css/font-awesome.css",
                "~/bower_components/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css",
                "~/Content/Styles/site.min.css"));
        }
    }
}
