namespace ProcessingTools.Web.Documents.Areas.Articles
{
    using System.Web.Mvc;
    using ProcessingTools.Common.Constants.Web;

    public class ArticlesAreaRegistration : AreaRegistration
    {
        public const string DefaultActionName = RouteConstants.DefaultActionName;

        public override string AreaName => AreasConstants.ArticlesAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Articles_default",
                "Articles/{controller}/{action}/{id}",
                new
                {
                    action = DefaultActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}