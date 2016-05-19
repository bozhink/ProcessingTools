namespace ProcessingTools.Bio.Taxonomy.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    public class TaxonName
    {
        private ICollection<TaxonRank> ranks;
        private bool whiteListed;

        /// <summary>
        /// Creates new TaxonName object.
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
        /// Gets or sets the value of the taxona name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfTaxonName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the condition if this TaxonName must appear
        /// in the white list for taxon tagging.
        /// </summary>
        public bool WhiteListed
        {
            get
            {
                return this.whiteListed;
            }

            set
            {
                this.whiteListed = value;
            }
        }

        public virtual ICollection<TaxonRank> Ranks { get; set; }
    }
}
