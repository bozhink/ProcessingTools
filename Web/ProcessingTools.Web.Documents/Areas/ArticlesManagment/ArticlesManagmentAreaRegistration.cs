namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment
{
    using System.Web.Mvc;

    public class ArticlesManagmentAreaRegistration : AreaRegistration
    {
        public override string AreaName => "ArticlesManagment";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ArticlesManagment_default",
                "ArticlesManagment/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}