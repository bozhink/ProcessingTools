namespace ProcessingTools.Layout.Processors.Models.Taggers
{
    public interface IContentTaggerSettings
    {
        bool CaseSensitive { get; }

        bool MinimalTextSelect { get; }
    }
}
