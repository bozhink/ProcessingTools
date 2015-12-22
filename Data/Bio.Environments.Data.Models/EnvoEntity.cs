namespace ProcessingTools.Bio.Environments.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EnvoEntity
    {
        private ICollection<EnvoName> envoNames;

        public EnvoEntity()
        {
            this.envoNames = new HashSet<EnvoName>();
        }

        [Key]
        [MinLength(10)]
        [MaxLength(10)]
        public string Id { get; set; }

        public int Index { get; set; }

        [MinLength(13)]
        [MaxLength(13)]
        public string EnvoId { get; set; }

        public virtual ICollection<EnvoName> EnvoNames
        {
            get
            {
                return this.envoNames;
            }

            set
            {
                this.envoNames = value;
            }
        }
    }
}