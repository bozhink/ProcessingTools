namespace ProcessingTools.Xml.Transform
{
    using System.IO;
    using System.Xml;
    using Saxon.Api;

    public class XQueryTransform
    {
        private Processor processor;
        private XQueryCompiler compiler;
        private XQueryEvaluator evaluator;

        public XQueryTransform()
        {
            this.processor = new Processor();
            this.compiler = this.processor.NewXQueryCompiler();
            this.evaluator = null;
        }

        public void Load(string query)
        {
            this.evaluator = this.compiler.Compile(query).Load();
        }

        public void Load(Stream queryStream)
        {
            this.evaluator = this.compiler.Compile(queryStream).Load();
        }

        public XmlDocument Evaluate(XmlNode node)
        {
            var nodeReader = new XmlNodeReader(node);
            var documentBuilder = this.processor.NewDocumentBuilder();

            var document = documentBuilder.Build(nodeReader);
            this.evaluator.ContextItem = document;

            var destination = new DomDestination();
            this.evaluator.Run(destination);

            return destination.XmlDocument;
        }
    }
}