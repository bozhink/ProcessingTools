namespace ProcessingTools.Resources.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using ProcessingTools.Resources.Data.Common.Models.Contracts;

    public abstract class EntityWithSources : IEntityWithSources
    {
        private ICollection<SourceId> sources;

        public EntityWithSources()
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
        ICollection<ISourceIdEntity> IEntityWithSources.Sources => this.Sources.ToList<ISourceIdEntity>();
    }
}
