namespace ProcessingTools.Contracts.ViewModels
{
    public interface IPageViewModel<T> : IPageViewModel
    {
        T Model { get; }
    }
}
