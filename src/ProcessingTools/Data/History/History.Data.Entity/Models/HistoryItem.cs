namespace ProcessingTools.History.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.History;
    using ProcessingTools.Models.Contracts.History;

    public class HistoryItem : IHistoryItem
    {
        public HistoryItem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateModified = DateTime.UtcNow;
        }

        [Key]
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public DateTime DateModified { get; set; }

        [Index]
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfObjectId)]
        public string ObjectId { get; set; }

        [Index]
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfObjectType)]
        public string ObjectType { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserId)]
        public string UserId { get; set; }
    }
}
