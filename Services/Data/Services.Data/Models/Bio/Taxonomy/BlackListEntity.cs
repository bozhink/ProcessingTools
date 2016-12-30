namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;

    internal class BlackListEntity : IBlackListEntity
    {
        public string Content { get; set; }
    }
}
