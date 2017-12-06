namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Contracts.Models.Resources;

    public abstract class EntityWithSources : IEntityWithSources
    {
        private ICollection<SourceId> sources;

        protected EntityWithSources()
        {
            this.sources = new HashSet<SourceId>();
        }

        public virtual ICollection<SourceId> Sources
        {
            get
            {
                return this.sources;
            }

            set
            {
                this.sources = value;
            }
        }

        [NotMapped]
        IEnumerable<ISourceIdEntity> IEntityWithSources.Sources => this.Sources;
    }
}
