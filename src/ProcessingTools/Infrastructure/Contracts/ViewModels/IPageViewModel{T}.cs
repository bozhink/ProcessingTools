namespace ProcessingTools.Contracts.ViewModels
{
    public interface IPageViewModel<out T> : IPageViewModel
    {
        T Model { get; }
    }
}
