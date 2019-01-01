namespace ProcessingTools.Data.Models.Entity.Bio.Taxonomy
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class BlackListEntity : IBlackListItem
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfBlackListedItemContent)]
        public string Content { get; set; }
    }
}
