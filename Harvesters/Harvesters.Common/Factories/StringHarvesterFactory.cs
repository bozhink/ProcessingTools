namespace ProcessingTools.Harvesters.Common.Factories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    public abstract class StringHarvesterFactory : IStringHarvester
    {
        public StringHarvesterFactory()
        {
            this.Items = new HashSet<string>();
        }

        public IQueryable<string> Data
        {
            get
            {
                return this.Items.AsQueryable();
            }
        }

        protected ICollection<string> Items { get; set; }

        public abstract void Harvest(string content);
    }
}