namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Common.Constants;

    public class BioTaxonomyDataAreaRegistration : AreaRegistration
    {
        public override string AreaName => AreasConstants.BioTaxonomyDataAreaName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BioTaxonomyData_default",
                "Data/Bio/Taxonomy/{controller}/{action}/{id}",
                new
                {
                    action = RouteConstants.DefaultActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
