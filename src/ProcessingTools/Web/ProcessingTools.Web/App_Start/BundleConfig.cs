namespace ProcessingTools.Web
{
    using System.Web.Optimization;
    using ProcessingTools.Web.Common.Constants;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle(BundleNames.JQueryScripts).Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle(BundleNames.JQueryValidationScripts).Include(
                    "~/Scripts/jquery.validate*"));

            bundles.Add(
                new ScriptBundle(BundleNames.ModernizrScripts).Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle(BundleNames.KnockoutScripts).Include(
                    "~/Scripts/knockout-{version}.js",
                    "~/Scripts/knockout.validation.js"));

            bundles.Add(
                new ScriptBundle(BundleNames.BootstrapScripts).Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js"));

            bundles.Add(
                new ScriptBundle(BundleNames.DefaultScripts).Include(
                    "~/Scripts/Site.js",
                    "~/Scripts/JsonRequester.js"));

            bundles.Add(
                new StyleBundle(BundleNames.DefaultStyles).Include(
                    "~/Content/bootstrap.css",
                    "~/Content/Site.css"));
        }
    }
}
