namespace ProcessingTools.Web.Documents.Areas.Data
{
    using System.Web.Mvc;

    public class DataAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Data";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Data_default",
                "Data/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
