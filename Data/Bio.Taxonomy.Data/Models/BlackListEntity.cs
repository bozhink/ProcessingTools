namespace ProcessingTools.Bio.Taxonomy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;

    public class BlackListEntity : IBlackListEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfBlackListedItemContent)]
        public string Content { get; set; }
    }
}
