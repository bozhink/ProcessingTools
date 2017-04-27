namespace ProcessingTools.Web.Common.ViewModels.Contracts
{
    public interface IPageViewModel<T> : IPageViewModel
    {
        T Model { get; }
    }
}
