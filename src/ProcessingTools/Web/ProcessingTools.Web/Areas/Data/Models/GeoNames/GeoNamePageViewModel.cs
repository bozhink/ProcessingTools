namespace ProcessingTools.Web.Areas.Data.Models.GeoNames
{
    using ProcessingTools.Contracts.ViewModels;

    public class GeoNamePageViewModel : IPageViewModel<GeoNameViewModel>
    {
        public GeoNameViewModel Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
