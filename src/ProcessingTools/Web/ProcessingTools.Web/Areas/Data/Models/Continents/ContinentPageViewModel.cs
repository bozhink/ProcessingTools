namespace ProcessingTools.Web.Areas.Data.Models.Continents
{
    using ProcessingTools.Contracts.ViewModels;

    public class ContinentPageViewModel : IPageViewModel<ContinentViewModel>
    {
        public ContinentViewModel Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
