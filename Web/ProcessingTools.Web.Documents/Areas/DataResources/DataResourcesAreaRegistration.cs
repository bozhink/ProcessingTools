namespace ProcessingTools.Web.Documents.Areas.DataResources
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Common.Constants;

    public class DataResourcesAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreasConstants.DataResourcesAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DataResources_default",
                "Data/Resources/{controller}/{action}/{id}",
                new
                {
                    action = ActionNames.DeafultIndexActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
