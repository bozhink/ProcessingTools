namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class TaxaContext
    {
        public TaxaContext()
        {
            this.Taxa = new ConcurrentQueue<Taxon>();
        }

        protected ConcurrentQueue<Taxon> Taxa { get; set; }

        public Task<IQueryable<Taxon>> All()
        {
            return Task.FromResult(new HashSet<Taxon>(this.Taxa).AsQueryable());
        }

        public Task<object> Add(Taxon taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Update(Taxon taxon) => Task.Run<object>(() => this.Upsert(taxon));

        public Task<object> Delete(Taxon taxon)
        {
            return Task.Run<object>(() =>
            {
                var taxa = this.Taxa.ToList();
                var result = taxa.RemoveAll(t => t.Name == taxon.Name);
                this.Taxa = new ConcurrentQueue<Taxon>(taxa);
                return result;
            });
        }

        private int Upsert(Taxon taxon)
        {
            Func<Taxon, bool> filter = t => t.Name == taxon.Name;

            try
            {
                if (this.Taxa.Count(filter) > 0)
                {
                    var item = this.Taxa.FirstOrDefault(filter);
                    foreach (var rank in taxon.Ranks)
                    {
                        item.Ranks.Add(rank);
                    }

                    return 0;
                }
                else
                {
                    this.Taxa.Enqueue(taxon);
                    return 1;
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
