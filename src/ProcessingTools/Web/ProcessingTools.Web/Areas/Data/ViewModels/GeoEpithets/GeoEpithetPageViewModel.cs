namespace ProcessingTools.Web.Areas.Data.ViewModels.GeoEpithets
{
    using ProcessingTools.Contracts.ViewModels;

    public class GeoEpithetPageViewModel : IPageViewModel<GeoEpithetViewModel>
    {
        public GeoEpithetViewModel Model { get; set; }

        public string PageHeading => this.PageTitle;

        public string PageTitle { get; set; }

        public string ReturnUrl { get; set; }
    }
}
