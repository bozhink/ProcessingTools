namespace ProcessingTools.Contracts.ViewModels
{
    public interface IPageViewModel
    {
        string PageTitle { get; }

        string PageHeading { get; }

        string ReturnUrl { get; }
    }
}
