namespace ProcessingTools.Web.Documents.Areas.Files
{
    using System.Web.Mvc;
    using ProcessingTools.Common.Constants.Web;

    public class FilesAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreasConstants.FilesAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Files_default",
                "Files/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}
