namespace ProcessingTools.WebApp.Areas.ProcessPage
{
    using System.Web.Mvc;

    public class ProcessPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ProcessPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProcessPage_default",
                "ProcessPage/{controller}/{action}/{id}",
                new { controller = "ProcessPage", action = "Index", id = UrlParameter.Optional });
        }
    }
}