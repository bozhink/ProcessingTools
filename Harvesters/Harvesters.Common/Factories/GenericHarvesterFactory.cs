namespace ProcessingTools.Harvesters.Common.Factories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    public abstract class GenericHarvesterFactory<T> : IHarvester<T>
    {
        public GenericHarvesterFactory()
        {
            this.Items = new HashSet<T>();
        }

        public IQueryable<T> Data
        {
            get
            {
                return this.Items.AsQueryable();
            }
        }

        protected ICollection<T> Items { get; set; }

        public abstract void Harvest(string content);
    }
}
