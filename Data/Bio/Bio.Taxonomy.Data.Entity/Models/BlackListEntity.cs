namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Bio.Taxonomy;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;

    public class BlackListEntity : IBlackListEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfBlackListedItemContent)]
        public string Content { get; set; }
    }
}
