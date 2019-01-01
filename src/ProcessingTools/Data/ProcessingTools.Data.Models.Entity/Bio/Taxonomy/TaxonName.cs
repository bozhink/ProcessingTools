namespace ProcessingTools.Data.Models.Entity.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    public class TaxonName
    {
        private ICollection<TaxonRank> ranks;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonName"/> class.
        /// </summary>
        public TaxonName()
        {
            this.ranks = new HashSet<TaxonRank>();
            this.WhiteListed = false;
        }

        /// <summary>
        /// Gets or sets the key field of the TaxonName.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value of the taxon name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfTaxonName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the condition if this TaxonName must appear
        /// in the white list for taxon tagging.
        /// </summary>
        public bool WhiteListed { get; set; }

        public virtual ICollection<TaxonRank> Ranks
        {
            get
            {
                return this.ranks;
            }

            set
            {
                this.ranks = value;
            }
        }
    }
}
