namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;

    public class TaxonRankEntity : ITaxonRankEntity
    {
        public TaxonRankEntity()
        {
            this.Ranks = new HashSet<TaxonRankType>();
        }

        public TaxonRankEntity(TaxonEntity entity)
            : this()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Name = entity.Name;
            this.IsWhiteListed = entity.IsWhiteListed;
            foreach (var rank in entity.Ranks)
            {
                this.Ranks.Add(rank.MapTaxonRankStringToTaxonRankType());
            }
        }

        public string Name { get; set; }

        public bool IsWhiteListed { get; set; }

        public ICollection<TaxonRankType> Ranks { get; private set; }
    }
}
