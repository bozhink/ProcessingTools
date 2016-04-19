namespace ProcessingTools.Web.Documents.Areas.JournalsManagment
{
    using System.Web.Mvc;

    public class JournalsManagmentAreaRegistration : AreaRegistration
    {
        public override string AreaName => "JournalsManagment";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "JournalsManagment_default",
                "JournalsManagment/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}