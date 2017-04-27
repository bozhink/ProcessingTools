namespace ProcessingTools.Web.Areas.Data
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Constants;

    public class DataAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreaNames.Data;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: RouteNames.DataDefault,
                url: "Data/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = RouteValues.DefaultContollerName,
                    action = RouteValues.IndexActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
