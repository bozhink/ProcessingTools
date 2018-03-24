// <copyright file="XmlSerializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using ProcessingTools.Contracts.Serialization;

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
                PreserveWhitespace = true
            };
        }

        /// <inheritdoc/>
        public Task<XmlNode> SerializeAsync(T @object)
        {
            return Task.Run(() =>
            {
                if (@object == null)
                {
                    return null;
                }

                return this.Serialize(@object);
            });
        }

        /// <inheritdoc/>
        public void SetNamespaces(XmlNamespaceManager namespaceManager)
        {
            if (namespaceManager == null)
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

        private XmlNode Serialize(T @object)
        {
            using (var stream = new MemoryStream())
            {
                if (this.xmlns == null)
                {
                    this.serializer.Serialize(stream, @object);
                }
                else
                {
                    this.serializer.Serialize(stream, @object, this.xmlns);
                }

                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream);
                this.bufferXml.LoadXml(reader.ReadToEnd());

                stream.Close();
            }

            return this.bufferXml.DocumentElement.CloneNode(true);
        }
    }
}
