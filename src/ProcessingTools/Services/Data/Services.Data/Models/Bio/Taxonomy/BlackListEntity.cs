namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    internal class BlackListEntity : IBlackListEntity
    {
        public string Content { get; set; }
    }
}
