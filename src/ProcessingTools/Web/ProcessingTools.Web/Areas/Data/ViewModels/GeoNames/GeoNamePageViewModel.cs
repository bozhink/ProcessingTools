namespace ProcessingTools.Web.Areas.Data.ViewModels.GeoNames
{
    using ProcessingTools.Web.Common.ViewModels.Contracts;

    public class GeoNamePageViewModel : IPageViewModel<GeoNameViewModel>
    {
        public GeoNameViewModel Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
