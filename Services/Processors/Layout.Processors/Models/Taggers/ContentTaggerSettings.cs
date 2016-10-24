namespace ProcessingTools.Layout.Processors.Models.Taggers
{
    public class ContentTaggerSettings : IContentTaggerSettings
    {
        public bool CaseSensitive { get; set; } = true;

        public bool MinimalTextSelect { get; set; } = false;
    }
}
