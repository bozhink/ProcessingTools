namespace ProcessingTools.Web.Documents.Areas.Data
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Common.Constants;

    public class DataAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreasConstants.DataAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                RouteConstants.DataDefaultMapRouteName,
                "Data/{controller}/{action}/{id}",
                new
                {
                    action = RouteConstants.DefaultActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
