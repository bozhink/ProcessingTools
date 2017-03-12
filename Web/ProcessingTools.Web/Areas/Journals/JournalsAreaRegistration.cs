namespace ProcessingTools.Web.Areas.Journals
{
    using System.Web.Mvc;
    using Constants;

    public class JournalsAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreaNames.Journals;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: RouteNames.JournalsDefault,
                url: "Journals/{controller}/{action}/{id}",
                defaults: new
                {
                    action = RouteValues.IndexActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
