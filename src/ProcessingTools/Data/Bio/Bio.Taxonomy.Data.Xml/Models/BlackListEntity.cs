namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class BlackListEntity : IBlackListItem
    {
        public string Content { get; set; }
    }
}
