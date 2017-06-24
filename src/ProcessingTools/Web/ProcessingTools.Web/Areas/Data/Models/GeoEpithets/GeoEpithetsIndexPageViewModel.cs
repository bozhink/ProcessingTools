namespace ProcessingTools.Web.Areas.Data.Models.GeoEpithets
{
    using ProcessingTools.Contracts.ViewModels;
    using ProcessingTools.Models.ViewModels;

    public class GeoEpithetsIndexPageViewModel : IPageViewModel<ListWithPagingViewModel<GeoEpithetViewModel>>
    {
        public ListWithPagingViewModel<GeoEpithetViewModel> Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
