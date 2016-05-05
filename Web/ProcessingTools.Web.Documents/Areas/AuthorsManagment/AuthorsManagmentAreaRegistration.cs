namespace ProcessingTools.Web.Documents.Areas.AuthorsManagment
{
    using System.Web.Mvc;

    public class AuthorsManagmentAreaRegistration : AreaRegistration
    {
        public override string AreaName => "AuthorsManagment";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AuthorsManagment_default",
                "AuthorsManagment/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}