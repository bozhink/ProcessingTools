namespace ProcessingTools.Infrastructure.Transform
{
    using System;
    using System.IO;
    using System.Xml;
    using Saxon.Api;

    public class XQueryTransform
    {
        private Processor processor;
        private DocumentBuilder documentBuilder;
        private XQueryEvaluator evaluator;

        public XQueryTransform()
        {
            this.processor = new Processor();
            this.documentBuilder = this.processor.NewDocumentBuilder();
            this.evaluator = null;
        }

        public void Load(string query)
        {
            this.evaluator = this.processor.NewXQueryCompiler().Compile(query).Load();
        }

        public void Load(Stream queryStream)
        {
            this.evaluator = this.processor.NewXQueryCompiler().Compile(queryStream).Load();
        }

        public Stream Evaluate(XmlNode node)
        {
            this.evaluator.ContextItem = this.documentBuilder.Build(node);
            return this.Evaluate();
        }

        public Stream Evaluate(Uri uri)
        {
            this.evaluator.ContextItem = this.documentBuilder.Build(uri);
            return this.Evaluate();
        }

        private Stream Evaluate()
        {
            if (this.evaluator == null)
            {
                throw new NullReferenceException("Evaluator is null. Execute Load() method first.");
            }

            var stream = new MemoryStream();

            Serializer serializer = new Serializer();
            serializer.SetOutputStream(stream);

            this.evaluator.Run(serializer);

            return stream;
        }
    }
}