namespace ProcessingTools.Web.Areas.Data.Models.GeoNames
{
    using ProcessingTools.Contracts.ViewModels;
    using ProcessingTools.Web.Models.Shared;

    public class GeoNamesIndexPageViewModel : IPageViewModel<ListWithPagingViewModel<GeoNameViewModel>>
    {
        public ListWithPagingViewModel<GeoNameViewModel> Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
