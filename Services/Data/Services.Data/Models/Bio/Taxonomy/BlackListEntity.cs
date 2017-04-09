namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;

    internal class BlackListEntity : IBlackListEntity
    {
        public string Content { get; set; }
    }
}
