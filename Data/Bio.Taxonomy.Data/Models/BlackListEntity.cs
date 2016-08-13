namespace ProcessingTools.Bio.Taxonomy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    public class BlackListEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfBlackListedItemContent)]
        public string Content { get; set; }
    }
}
