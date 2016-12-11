namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;

    internal class BlackListEntity : IBlackListEntity
    {
        public string Content { get; set; }
    }
}
