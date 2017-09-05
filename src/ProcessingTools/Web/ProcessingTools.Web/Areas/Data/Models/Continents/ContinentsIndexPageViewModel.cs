namespace ProcessingTools.Web.Areas.Data.Models.Continents
{
    using ProcessingTools.Contracts.ViewModels;
    using ProcessingTools.Web.Models.Shared;

    public class ContinentsIndexPageViewModel : IPageViewModel<ListWithPagingViewModel<ContinentViewModel>>
    {
        public ListWithPagingViewModel<ContinentViewModel> Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
