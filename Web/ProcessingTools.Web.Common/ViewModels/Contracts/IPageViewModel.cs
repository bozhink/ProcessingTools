namespace ProcessingTools.Web.Common.ViewModels.Contracts
{
    public interface IPageViewModel
    {
        string PageTitle { get; }

        string PageHeading { get; }

        string ReturnUrl { get; }
    }
}
