// <copyright file="XmlSerializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// Generic XML serializer.
    /// </summary>
    /// <typeparam name="T">Type of serialization model.</typeparam>
    public class XmlSerializer<T> : IXmlSerializer<T>
    {
        private readonly XmlSerializer serializer;
        private readonly XmlDocument bufferXml;
        private XmlSerializerNamespaces xmlns;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializer{T}"/> class.
        /// </summary>
        public XmlSerializer()
        {
            this.serializer = new XmlSerializer(typeof(T));
            this.xmlns = null;

            this.bufferXml = new XmlDocument
            {
                PreserveWhitespace = true,
            };
        }

        /// <inheritdoc/>
        public XmlNode Serialize(T source)
        {
            if (source is null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                if (this.xmlns is null)
                {
                    this.serializer.Serialize(stream, source);
                }
                else
                {
                    this.serializer.Serialize(stream, source, this.xmlns);
                }

                stream.Flush();
                stream.Position = 0;

                using (var reader = new StreamReader(stream, Defaults.Encoding, true, 4096, true))
                {
                    this.bufferXml.LoadXml(reader.ReadToEnd());
                }

                stream.Close();
            }

            return this.bufferXml.DocumentElement.CloneNode(true);
        }

        /// <inheritdoc/>
        public void SetNamespaces(XmlNamespaceManager namespaceManager)
        {
            if (namespaceManager is null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            this.xmlns = new XmlSerializerNamespaces();
            var ns = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.All);
            foreach (var prefix in ns.Keys)
            {
                this.xmlns.Add(prefix, ns[prefix]);
            }
        }
    }
}
