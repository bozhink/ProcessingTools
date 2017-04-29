namespace ProcessingTools.Web.Areas.Data.ViewModels.Continents
{
    using ProcessingTools.Web.Common.ViewModels.Contracts;

    public class ContinentPageViewModel : IPageViewModel<ContinentViewModel>
    {
        public ContinentViewModel Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}