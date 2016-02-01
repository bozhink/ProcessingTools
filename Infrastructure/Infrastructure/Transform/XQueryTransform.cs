namespace ProcessingTools.Infrastructure.Transform
{
    using System.IO;
    using System.Xml;
    using Saxon.Api;

    public class XQueryTransform
    {
        private DocumentBuilder documentBuilder;
        private XQueryEvaluator evaluator;

        public XQueryTransform(string query)
        {
            var processor = new Processor();
            this.documentBuilder = processor.NewDocumentBuilder();
            this.evaluator = processor.NewXQueryCompiler().Compile(query).Load();
        }

        public XQueryTransform(Stream queryStream)
        {
            var processor = new Processor();
            this.documentBuilder = processor.NewDocumentBuilder();
            this.evaluator = processor.NewXQueryCompiler().Compile(queryStream).Load();
        }

        public Stream Evaluate(XmlNode node)
        {
            this.evaluator.ContextItem = this.documentBuilder.Build(node);

            var stream = new MemoryStream();

            Serializer serializer = new Serializer();
            serializer.SetOutputStream(stream);

            this.evaluator.Run(serializer);

            return stream;
        }
    }
}