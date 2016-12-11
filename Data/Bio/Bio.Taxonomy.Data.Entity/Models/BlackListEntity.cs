namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;

    public class BlackListEntity : IBlackListEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfBlackListedItemContent)]
        public string Content { get; set; }
    }
}
