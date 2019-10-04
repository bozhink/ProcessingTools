// <copyright file="XQueryTransform.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Xml.XQuery
{
    using System.IO;
    using System.Xml;
    using Saxon.Api;

    /// <summary>
    /// XQuery transform.
    /// </summary>
    public class XQueryTransform : IXQueryTransform
    {
        private readonly Processor processor;
        private readonly XQueryCompiler compiler;
        private XQueryEvaluator evaluator;

        /// <summary>
        /// Initializes a new instance of the <see cref="XQueryTransform"/> class.
        /// </summary>
        public XQueryTransform()
        {
            this.processor = new Processor();
            this.compiler = this.processor.NewXQueryCompiler();
            this.evaluator = null;
        }

        /// <inheritdoc/>
        public void Load(string query)
        {
            this.evaluator = this.compiler.Compile(query).Load();
        }

        /// <inheritdoc/>
        public void Load(Stream queryStream)
        {
            this.evaluator = this.compiler.Compile(queryStream).Load();
        }

        /// <inheritdoc/>
        public XmlDocument Evaluate(XmlNode node)
        {
            var documentBuilder = this.processor.NewDocumentBuilder();

            using (var nodeReader = new XmlNodeReader(node))
            {
                var document = documentBuilder.Build(nodeReader);
                this.evaluator.ContextItem = document;
            }

            var destination = new DomDestination();
            this.evaluator.Run(destination);

            return destination.XmlDocument;
        }
    }
}
