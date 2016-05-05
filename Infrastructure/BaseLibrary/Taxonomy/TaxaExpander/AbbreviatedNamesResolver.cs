namespace ProcessingTools.BaseLibrary.Taxonomy.TaxaExpander
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    public class AbbreviatedNamesResolver
    {
        public Task Resolve(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                // TODO
            });
        }
    }
}
