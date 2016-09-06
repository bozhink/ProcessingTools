namespace ProcessingTools.Web.Documents.Areas.Journals
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Common.Constants;

    public class JournalsAreaRegistration : AreaRegistration
    {
        public const string DefaultActionName = RouteConstants.DefaultActionName;

        public override string AreaName => AreasConstants.JournalsAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Journals_default",
                "Journals/{controller}/{action}/{id}",
                new
                {
                    action = DefaultActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
