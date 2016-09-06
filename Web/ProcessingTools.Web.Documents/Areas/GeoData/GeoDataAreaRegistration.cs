namespace ProcessingTools.Web.Documents.Areas.GeoData
{
    using System.Web.Mvc;

    public class GeoDataAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GeoData";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "GeoData_default",
                "Data/Geo/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}
