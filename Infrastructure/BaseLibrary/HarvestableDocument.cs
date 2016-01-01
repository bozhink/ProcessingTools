namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Configurator;
    using Contracts;
    using Extensions;

    public class HarvestableDocument : Base
    {
        private string textContent;
        private IEnumerable<string> textWords;

        public HarvestableDocument(Config config, string xml)
            : base(config, xml)
        {
            this.Initialize();
        }

        public HarvestableDocument(IBase baseObject)
            : base(baseObject)
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets the text content of the xml document.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Throws when the xml document does not contain text content.</exception>
        public string TextContent
        {
            get
            {
                if (this.textContent == null || this.NeedsUpdate)
                {
                    this.SetTextContent();
                    this.NeedsUpdate = false;
                }

                return this.textContent;
            }

            private set
            {
                if (value == null || value.Length < 1)
                {
                    throw new ArgumentNullException("This document does not contain valid text content.");
                }

                this.textContent = value;
            }
        }

        /// <summary>
        /// Gets the HashSet of words in the xml document.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Throws when the xml document does not contain valid words.</exception>
        public IEnumerable<string> TextWords
        {
            get
            {
                if (this.textWords == null || this.textWords.Count() < 1 || this.NeedsUpdate)
                {
                    this.SetTextContent();
                    this.NeedsUpdate = false;
                }

                return this.textWords;
            }

            private set
            {
                if (value == null || value.Count() < 1)
                {
                    throw new ArgumentNullException("This document does not contain valid words.");
                }

                this.textWords = value;
            }
        }

        private void Initialize()
        {
            this.textContent = null;
            this.textWords = new HashSet<string>();
        }

        private void SetTextContent()
        {
            if (this.Config == null)
            {
                throw new NullReferenceException("Null config.");
            }

            string text = this.XmlDocument.ApplyXslTransform(this.Config.TextContentXslFileName);
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            this.TextContent = text;
            this.TextWords = text.ExtractWordsFromString();
        }
    }
}