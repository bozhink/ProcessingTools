namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class BlackListEntity : IBlackListItem
    {
        public string Content { get; set; }
    }
}
