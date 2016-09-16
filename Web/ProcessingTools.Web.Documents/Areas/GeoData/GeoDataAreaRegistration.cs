namespace ProcessingTools.Web.Documents.Areas.GeoData
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Common.Constants;

    public class GeoDataAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreasConstants.GeoDataAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "GeoData_default",
                "Data/Geo/{controller}/{action}/{id}",
                new
                {
                    action = ActionNames.DeafultIndexActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
