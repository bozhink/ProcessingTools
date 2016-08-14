namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;

    internal class BlackListEntity : IBlackListEntity
    {
        public string Content { get; set; }
    }
}
