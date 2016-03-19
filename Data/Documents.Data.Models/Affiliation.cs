namespace ProcessingTools.Documents.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Affiliation
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfAffiliationName)]
        public string Name { get; set; }

        public virtual int InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual int AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}