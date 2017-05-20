namespace ProcessingTools.Web.Areas.Data.ViewModels.Continents
{
    using ProcessingTools.Contracts.ViewModels;
    using ProcessingTools.Web.Common.ViewModels;

    public class ContinentIndexPageViewModel : IPageViewModel<ListWithPagingViewModel<ContinentViewModel>>
    {
        public ListWithPagingViewModel<ContinentViewModel> Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
