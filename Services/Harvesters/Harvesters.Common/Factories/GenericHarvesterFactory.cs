//namespace ProcessingTools.Harvesters.Common.Factories
//{
//    using System.Linq;
//    using System.Threading.Tasks;
//    using System.Xml;

//    using Contracts;

//    public abstract class GenericHarvesterFactory<T> : IHarvester<T>
//    {
//        public Task<IQueryable<T>> Harvest(XmlNode context)
//        {
//            XmlDocument document = new XmlDocument
//            {
//                PreserveWhitespace = true
//            };

//            document.LoadXml(Resources.ContextWrapper);

//            document.DocumentElement.InnerXml = context.InnerXml;

//            return this.Run(document);
//        }

//        protected abstract Task<IQueryable<T>> Run(XmlDocument document);
//    }
//}
